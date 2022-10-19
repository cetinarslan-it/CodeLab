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
                insuranceItem.Depth = 0;
                var itemSum = 0;
                itemSum += insuranceItem.Value;

                if (insuranceItem.Depth == maxDepth || insuranceItem.Children.Count() == 0)
                {
                    insuranceArr[i] = itemSum;
                    i++;
                    continue;
                }

                foreach (var childDepth1 in insuranceItem.Children)
                {
                    childDepth1.Depth = insuranceItem.Depth + 1;
                    itemSum += childDepth1.Value;

                    if (childDepth1.Depth == maxDepth || childDepth1.Children.Count() == 0)
                    {
                        insuranceArr[i] = itemSum;
                        continue;
                    }

                    foreach (var childDepth2 in childDepth1.Children)
                    {
                        childDepth2.Depth = childDepth1.Depth + 1;
                        itemSum += childDepth2.Value;

                        if (childDepth2.Depth == maxDepth || childDepth2.Children.Count() == 0)
                        {
                            insuranceArr[i] = itemSum;
                            continue;
                        }

                        foreach (var childDepth3 in childDepth2.Children)
                        {
                            childDepth3.Depth = childDepth2.Depth + 1;
                            itemSum += childDepth3.Value;

                            if (childDepth3.Depth == maxDepth || childDepth3.Children.Count() == 0)
                            {
                                insuranceArr[i] = itemSum;
                                continue;
                            }

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