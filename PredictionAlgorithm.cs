using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LinkedInRecommendation
{
    class PredictionAlgorithm
    {
        private List<Person> _lConnections;

        /// <summary>
        /// Constructor
        /// </summary>
        public PredictionAlgorithm ()
        {
        }
        /// <summary>
        /// Setter of list connections
        /// </summary>
        /// <param name="lConnections"></param>
        public void setLConnections(List<Person> lConnections)
        {
            this._lConnections = lConnections;
        }

        /// <summary>
        /// Main prediction algorithm for assessing who have a better chance of moving to San Francisco
        /// For our simple algorithm, we will assume that we could separate each aspect of the data out
        /// and score them separately. In real life, one category of the data would affect the other
        /// category. eg. if a person is from MIT && is a new grad && is interested in startup, he has
        /// a high chance of moving to SF.
        /// </summary>
        public void prediction()
        {
            // First score based on location
            locationScore();

            // Second score based on headline
            headlineScore();

            // Third score based on industry
            industryScore();


        }

        /// <summary>
        /// Best way to do this, is to based on the nationality of the person, if we have a way of
        /// matching user's first and last name to a database of nationality (Kim from Korea, Vladmir
        /// from Russia), then we could rank based on that since according to data, Caucasian, Asian,
        /// and Indian has the highest chance of moving to SF. Since we are under time constrain here,
        /// we will only score based on geolocation as follows:
        /// people from US (not from San Francisco) have a higher chance of
        /// moving to SF than people from Canada than elsewhere. (since it will be more expensive to relocate)
        /// 20 points for people from US not from San Francisco
        /// 10 point for people from Canada
        /// </summary>
        private void locationScore()
        {
            for (int i = 0; i < _lConnections.Count; i++ )
            {
                string ctry = _lConnections[i].locationCountry.ToLower();
                string city = _lConnections[i].locationCity.ToLower();
                if ((ctry == "us") && (!city.Contains("san francisco")))
                {
                    _lConnections[i].score += 20;
                    _lConnections[i].score_aspects.Add(Person.eScore.location_high);
                }
                else if (ctry == "ca")
                {
                    _lConnections[i].score += 10;
                    _lConnections[i].score_aspects.Add(Person.eScore.location_low);
                }
                else
                {
                    _lConnections[i].score_aspects.Add(Person.eScore.location_none);
                }
            }
        }

        /// <summary>
        /// Best way to do this, is to have natural language processing to try to understand what the person
        /// is most interested in. Based on that, we could better determine if he is an entrepreneur, or if
        /// he is actively seeking jobs in SF. Again, since we are under time constrain here, we will only
        /// score based on keywords as follows:
        /// people having keywords like "entrepreneur" or "startup" have a higher chance of moving to SF than
        /// people having keywords like "new grad" or "student" or "phD" than people having keywords like
        /// "software" or "engineer" or "developer". Note that these are non-inclusive, so if a person is
        /// interested in "startup" and is a "new grad" and is a "engineer", he will be added 42 points
        ///  21 points for people in category A
        ///  14 point for people in category B
        ///  7 point for people in category C
        /// </summary>
        private void headlineScore()
        {
            for (int i = 0; i < _lConnections.Count; i++)
            {
                string hdline = _lConnections[i].headline.ToLower();
                bool isSelected = false;
                if ((hdline.Contains("entrepreneur")) || (hdline.Contains("startup")) || (hdline.Contains("start-up")))
                {
                    _lConnections[i].score += 21;
                    _lConnections[i].score_aspects.Add(Person.eScore.headline_high);
                    isSelected = true;
                }
                if ((hdline.Contains("new grad")) || (hdline.Contains("student")) || (hdline.Contains("phD")))
                {
                    _lConnections[i].score += 14;
                    _lConnections[i].score_aspects.Add(Person.eScore.headline_medium);
                    isSelected = true;
                }
                if ((hdline.Contains("software")) || (hdline.Contains("engineer")) || (hdline.Contains("developer")))
                {
                    _lConnections[i].score += 7;
                    _lConnections[i].score_aspects.Add(Person.eScore.headline_low);
                    isSelected = true;
                }
                if (!isSelected)
                {
                    _lConnections[i].score_aspects.Add(Person.eScore.headline_none);
                }
            }
        }

        /// <summary>
        /// Best way to do this, is to have a comprehensive database about every industry, then we can match
        /// different industries to whether it contribute to moving to SF or not. We could also train the data
        /// set to see if a particular industry tend to move to SF or not. since we are under time constrain
        /// here, we will only score based on keywords as follows:
        /// people having keywords like "computer" or "software" or "hardware" have a higher chance of moving than
        /// people having keywords like "recruiting" or "staffing" or "human resource" than people having keywords like
        /// "marketing" or "information technology" or "design" or "logistic"
        ///  21 points for people in category A
        ///  14 point for people in category B
        ///  7 point for people in category C
        /// </summary>
        private void industryScore()
        {
            for (int i = 0; i < _lConnections.Count; i++)
            {
                string ind = _lConnections[i].industry.ToLower();
                if ((ind.Contains("computer")) || (ind.Contains("software")) || (ind.Contains("hardware")))
                {
                    _lConnections[i].score += 21;
                    _lConnections[i].score_aspects.Add(Person.eScore.industry_high);
                }
                else if ((ind.Contains("recruiting")) || (ind.Contains("staffing")) || (ind.Contains("human resource")))
                {
                    _lConnections[i].score += 14;
                    _lConnections[i].score_aspects.Add(Person.eScore.industry_medium);
                }
                else if ((ind.Contains("marketing")) || (ind.Contains("design")) || (ind.Contains("logistic")) || (ind.Contains("information technology")))
                {
                    _lConnections[i].score += 7;
                    _lConnections[i].score_aspects.Add(Person.eScore.industry_low);
                }
                else
                {
                    _lConnections[i].score_aspects.Add(Person.eScore.industry_none);
                }
            }
        }

        /// <summary>
        /// Check if _lConnections count is greater than 10
        /// </summary>
        /// <returns></returns>
        public int getCount()
        {
            return _lConnections.Count;
        }
        /// <summary>
        /// Return top ten of the highest scorer from the list of connections
        /// </summary>
        /// <returns>List<Person></returns>
        public List<Person> getTopTen()
        {
            var sortedList = _lConnections.OrderByDescending(d => d.score).ToList();

            List<Person> _lTopTen = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                _lTopTen.Add(sortedList[i]);
            }
            return _lTopTen;
        }


    }

    /// <summary>
    /// class for person to be used throughout the code
    /// </summary>
    class Person
    {
        public string firstName;
        public string lastName;
        public string headline;
        public string picUrl;
        public string locationCountry;
        public string locationCity;
        public string industry;
        public int score=0;
        public List<eScore> score_aspects = new List<eScore>();

        /// <summary>
        /// Score results for how each person is scored, used for displaying results in the end
        /// </summary>
        public enum eScore
        {
            location_high,
            location_low,
            location_none,
            headline_high,
            headline_medium,
            headline_low,
            headline_none,
            industry_high,
            industry_medium,
            industry_low,
            industry_none
        }

        /// <summary>
        /// Get color from score aspect
        /// location: RED
        /// high: 250, low: 150, none: 50
        /// headline: GREEN
        /// high: 250, med: 180, low: 120, none: 50
        /// industry: BLUE
        /// high: 250, med: 180, low: 120, none: 50
        /// </summary>
        /// <param name="leScore"></param>
        /// <returns></returns>
        public Color getColorFromScore(List<Person.eScore> leScore)
        {
            int r=0, g=0, b=0;
            foreach (Person.eScore s in leScore)
            {
                r = 0; g = 0; b = 0;
                if (s == Person.eScore.location_high) r += 250;
                else if (s == Person.eScore.location_low) r += 150;
                else if (s == Person.eScore.location_none) r += 50;
                if (s == Person.eScore.headline_high) g += 250;
                else if (s == Person.eScore.headline_medium) g += 180;
                else if (s == Person.eScore.headline_low) g += 120;
                else if (s == Person.eScore.headline_none) g += 50;
                if (s == Person.eScore.industry_high) b += 250;
                else if (s == Person.eScore.industry_medium) b += 180;
                else if (s == Person.eScore.industry_low) b += 120;
                else if (s == Person.eScore.industry_none) b += 50;
            }
            return Color.FromArgb(r, g, b);
        } 
    }
}
