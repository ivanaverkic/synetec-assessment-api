using Business.Dtos;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IBonusPoolService
    {
        Task<BonusPoolCalculatorResultDto> CalculateAsync(CalculateBonusDto calculateBonus);
    }
}
