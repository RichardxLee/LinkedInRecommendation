using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkedInRecommendation
{
    public partial class FormMain : Form
    {
        // Declare login helper, xml parser, and predictor
        OAuthUtilHelper _loginHelper;
        XMLParser _xmlHelper;
        PredictionAlgorithm _predictor;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Update status
            toolStripStatusLabel1.Text = "Initialization Complete, Please click on Login button";
            this.Text = "Welcome to LinkedIn Analytic!";
            // Initialize loginHelper, xmlHelper, and predictor
            _loginHelper = new OAuthUtilHelper();
            _xmlHelper = new XMLParser();
            _predictor = new PredictionAlgorithm();
        }

        /// <summary>
        /// Login function for going to next page of verification, while redirect web url to login screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // Update status
            tabControl1.SelectTab(tabVerify);
            toolStripStatusLabel1.Text = "Please login, and enter verification code into the textbox at bottom right corner";
            this.Text = "Login and Verification";
            // Get token and direct webbrowser to new link
            await _loginHelper.getRequestToken();
            string link = _loginHelper.getAuthorizeLink();
            if (string.IsNullOrEmpty(link))
                quitWithError("0e01: Application is quiting because failed to locate link for verification code ... ");
            webBrowser1.Navigate(new Uri(link));
        }

        /// <summary>
        /// Exit application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Verify code, go to next page of analytic and get response from logged in user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnVerify_Click(object sender, EventArgs e)
        {
            // Update status
            toolStripStatusLabel1.Text = "Verifying your account ... ";
            // Get verification code inputed from user
            string verificationCode = txtVerificationCode.Text;
            if (string.IsNullOrEmpty(verificationCode))
                quitWithError("0e02: Application is quiting because verification cannot be null or empty ... ");
            _loginHelper.setVerificationCode(verificationCode);
            await _loginHelper.getAccessToken();
            // Get user profile
            await _loginHelper.requestUserProfile();
            string userProfile = _loginHelper.getLinkedInData();
            if (string.IsNullOrEmpty(userProfile))
                quitWithError("0e03: Application is quiting because failed to get user profile ... ");
            // XML parse it
            Person p = _xmlHelper.parseUserProfile(userProfile);
            // Update status
            tabControl1.SelectTab(tabWait);
            toolStripStatusLabel1.Text = "Analyzing ... ";
            this.Text = "LinkedIn Analytic for " + p.firstName + " " + p.lastName;
            // Get user connections
            await _loginHelper.requestUserConnection();
            string userConnections = _loginHelper.getLinkedInData();
            if (string.IsNullOrEmpty(userConnections))
                quitWithError("0e04: Application is quiting because failed to get user connections ... ");
            // XML parse it, and store in predictor
            _predictor.setLConnections(_xmlHelper.parseUserConnection(userConnections));
            // Predict algorithm
            _predictor.prediction();
            if (_predictor.getCount() < 10)
                quitWithError("0e05: Application is quiting because connection count is less than 10 ... ");
            List<Person> lTopTen = _predictor.getTopTen();
            // Update status
            tabControl1.SelectTab(tabAnalytic);
            toolStripStatusLabel1.Text = "Analysis completed successfully!";
            // Display top ten results and render graphic
            renderPictureBox(lTopTen);
        }

        /// <summary>
        /// Render picture box for display
        /// </summary>
        /// <param name="lTopTen"></param>
        private void renderPictureBox(List<Person> lTopTen)
        {
            int x_start = 50, x_offset = 145, y_start = 85, y_offset = 200, nam_offset = 5, sco_offset = 15;
            int j = 0, k = 0;
            for (int i = 0; i < lTopTen.Count; i++)
            {
                // add face image to picture
                j = i;
                PictureBox pic = new PictureBox();
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pic.Width = 100;
                pic.Height = 100;
                if (j >= 5) { j -= 5; k = 1; }
                pic.Location = new Point(x_start + j * x_offset, y_start + k * y_offset);
                pic.ImageLocation = lTopTen[i].picUrl;
                tabAnalytic.Controls.Add(pic);
                // calculate rgb color based on scoring aspects
                Person pHelper = new Person();
                Color c = pHelper.getColorFromScore(lTopTen[i].score_aspects);
                // add color code to the side for how the score is calculated
                PictureBox col = new PictureBox();
                col.Width = 16;
                col.Height = 16;
                col.Location = new Point(x_start + pic.Width - col.Width + j * x_offset, y_start + k * y_offset);
                col.BackColor = c;
                tabAnalytic.Controls.Add(col);
                // add name below picture
                Label nam = new Label();
                nam.Location = new Point(x_start + j * x_offset, y_start + pic.Height + nam_offset + k * y_offset);
                nam.Text = "Name: " + lTopTen[i].firstName + " " + lTopTen[i].lastName;
                tabAnalytic.Controls.Add(nam);
                // add score below name
                Label sco = new Label();
                sco.Location = new Point(x_start + j * x_offset, y_start + pic.Height + nam_offset + sco_offset + k * y_offset);
                sco.Text = "Score: " + lTopTen[i].score.ToString();
                tabAnalytic.Controls.Add(sco);
                // refresh controls
                pic.BringToFront();
                col.BringToFront();
                nam.BringToFront();
                sco.BringToFront();
                pic.Refresh();
                col.Refresh();
                nam.Refresh();
                sco.Refresh();
            }

            lblExplanation.Text = "From analyzing your connections on LinkedIn, we have concluded the following ten people having the highest chance of moving to San Francisco!";
            lblColorExplanation.Text = "Following recommendations are reflected by colors which is based on Location:Red(255-0), Headline:Green(255-0), Industry:Blue(255-0)";
            tabAnalytic.Refresh();

        }

        /// <summary>
        /// Function to quit application with messagebox showing error
        /// </summary>
        /// <param name="message"></param>
        private void quitWithError(string message)
        {
            MessageBox.Show(message,
                        "Important Note",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);

            // In a real application, we would want to log the error in a file so later on support can
            // come back to inspect where went wrong

            Application.Exit();
        }
    }
}
