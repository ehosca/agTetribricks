using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.Services;

namespace agTetribricksWeb
{
    [ServiceContract(Namespace = "http://www.hosca.com/tetribricks")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ScoreService : WebService
    {
        [OperationContract]
        public List<TbScore> GetLatestScores()
        {
            return new tbscoresEntities().TbScores.OrderByDescending(s => s.ScoreValue).Take(20).ToList();
        }

        [OperationContract]
        public void SaveScore(TbScore score)
        {
            using (var dc = new tbscoresEntities())
            {
                score.ID = Guid.NewGuid();
                dc.AddToTbScores(score);
                dc.SaveChanges();
            }
        }
    }
}