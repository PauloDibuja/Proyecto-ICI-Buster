using System;
using System.Collections;
namespace Rating{
    public enum ESRB{eC, E, E10, T, M, A}
    public enum MPA{G, PG, PG13, R, NC17}

    public class RatingInfo{
        private Dictionary<string, MPA> mpa_ratings = new Dictionary<string, MPA>(){
            {"G", MPA.G},
            {"PG", MPA.PG},
            {"PG13", MPA.PG13},
            {"R", MPA.R},
            {"NC17", MPA.NC17}
        };
        private Dictionary<string, ESRB> esrb_ratings = new Dictionary<string, ESRB>(){
            {"eC", ESRB.eC},
            {"E", ESRB.E},
            {"E10", ESRB.E10},
            {"T", ESRB.T},
            {"M", ESRB.M},
            {"A", ESRB.A}
        };

        public Dictionary<string, MPA> MPA_Ratings{
            get { return this.mpa_ratings;}
        }
        public Dictionary<string, ESRB> ESRB_Ratings{
            get { return this.esrb_ratings;}
        }
    }
}