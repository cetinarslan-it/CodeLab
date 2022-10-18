using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Repositories;
using System.Linq;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController : Controller
    {
        private IRepository<Insurance> _repository;
        public InsuranceController(IRepository<Insurance> repository)
        {
            _repository = repository;
        }

        [HttpGet("top/{maxCount}/{maxDepth}")]
        public ActionResult<int[]> Top(int maxCount, int maxDepth)
        {
            var insuranceList = _repository.GetAll();
            var insuranceArr = new int[insuranceList.Count()];
            var i = 0;

            foreach (var insuranceItem in insuranceList)
            {
                var depth = maxDepth;
                var itemSum = 0;
                itemSum += insuranceItem.Value;

                if (insuranceItem.Children.Count() == 0 || (depth == 0 && insuranceItem.Children.Count() == 0 ))
                {
                    insuranceArr[i] = itemSum;
                    i++;
                    continue;
                }

                foreach (var childDepth1 in insuranceItem.Children)
                {                   
                    itemSum += childDepth1.Value;
                    insuranceArr[i] = itemSum;
                    depth--;

                    if (childDepth1.Children.Count() == 0 || (depth == 0 && childDepth1.Children.Count() == 0 )) continue;

                    foreach (var childDepth2 in childDepth1.Children)
                    {                       
                        itemSum += childDepth2.Value;
                        insuranceArr[i] = itemSum;
                        depth--;

                        if (childDepth2.Children.Count() == 0 || (depth == 0 && childDepth2.Children.Count() == 0 )) continue;

                        foreach (var childDepth3 in childDepth2.Children)
                        {                            
                            itemSum += childDepth3.Value;
                            insuranceArr[i] = itemSum;
                            depth--;

                            if (childDepth3.Children.Count() == 0 || (depth == 0 && childDepth3.Children.Count() == 0 )) continue;
                        }
                    }
                }

                i++;
            }

            var finalArray = insuranceArr.OrderByDescending(y => y).Take(maxCount).ToArray();

            return Ok(finalArray);
        }
    }
}