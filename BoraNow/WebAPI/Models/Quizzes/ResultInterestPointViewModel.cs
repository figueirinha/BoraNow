using Recodme.RD.BoraNow.DataLayer.Quizzes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.ResultInterestPointzes
{
    public class ResultInterestPointViewModel
    {
        public Guid Id { get; set; }
        public Guid ResultId { get; set; }
        public Guid InterestPointId { get; set; }

        public ResultInterestPoint ToResultInterestPoint()
        {
            return new ResultInterestPoint(ResultId, InterestPointId);
        }

        public static ResultInterestPointViewModel Parse(ResultInterestPoint resultInterestPoint)
        {
            return new ResultInterestPointViewModel()
            {
                Id = resultInterestPoint.Id,
                ResultId = resultInterestPoint.ResultId,
                InterestPointId = resultInterestPoint.InterestPointId
            };
        }
    }
}
