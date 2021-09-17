using Business.Dtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecAssessmentApi.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace SynetecAssessmentApi.Test
{
    public class CalculateBonusTest
    {
        [Fact]
        public async Task Test_CalculateBonus_Successful()
        {
            //Arrange
            var calcBonus = new CalculateBonusDto { TotalBonusPoolAmount = 10, SelectedEmployeeId = 2 };
            var dept = new DepartmentDto
            {
                Title = "Human Resources",
                Description = "The Human Resources department for the company"
            };
            var emp = new EmployeeDto
            {
                Fullname = "Janet Jones",
                JobTitle = "Hr Director",
                Salary = 90000,
                Department = dept
            };
            var bonusCalcResult = new BonusPoolCalculatorResultDto { Amount = 1, Employee = emp };

            var mockService = new Mock<IBonusPoolService>();
            mockService.Setup(m => m.CalculateAsync(calcBonus)).ReturnsAsync(bonusCalcResult);
            var bonusPoolController = new BonusPoolController(mockService.Object);

            //Act
            var result = await bonusPoolController.CalculateBonus(calcBonus);

            //Assert
            var okRequestResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okRequestResult);
            Assert.True(okRequestResult is OkObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okRequestResult.StatusCode);
            Assert.Equal(bonusCalcResult, okRequestResult.Value);
        }

        [Fact]
        public async Task Test_CalculateBonus_EmployeeNotFound()
        {
            //Arrange
            var calcBonus = new CalculateBonusDto { TotalBonusPoolAmount = 0, SelectedEmployeeId = 88880 };

            BonusPoolCalculatorResultDto bonusCalcResult = null;
            var mockService = new Mock<IBonusPoolService>();
            mockService.Setup(m => m.CalculateAsync(calcBonus)).ReturnsAsync(bonusCalcResult);
            var bonusPoolController = new BonusPoolController(mockService.Object);

            //Act
            var result = await bonusPoolController.CalculateBonus(calcBonus);

            //Assert
            var okRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, okRequestResult.StatusCode);
        }
    }
}
