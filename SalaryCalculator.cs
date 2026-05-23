using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SalaryCalculator
{
    public static decimal GetSalaryByPosition(string position)
    {
        switch (position)
        {
            case "Ассистент":
                return 3880m;
            case "Преподаватель":
                return 4268m;
            case "Старший преподаватель":
                return 5104m;
            case "Доцент":
                return 5247m;
            case "Профессор":
                return 6003m;
            case "Заведующий кафедрой":
                return 6463m;
            case "Декан факультета":
                return 6960m;
            default:
                return 0m;
        }
    }

    public static decimal CalculateAllowances(
        decimal salary,
        bool isDocent,
        bool isHarm,
        bool isCabinet,
        bool isNational,
        bool isCurator,
        bool isHonored,
        bool isCandidate,
        bool isDoctor,
        bool isMethodLit)
    {
        decimal allowances = 0m;

        if (isDocent)
            allowances += salary * 0.40m;

        if (isHarm)
            allowances += salary * 0.12m;

        if (isCabinet)
            allowances += salary * 0.10m;

        if (isNational)
            allowances += salary * 0.15m;

        if (isCurator)
            allowances += salary * 0.20m;

        if (isHonored)
            allowances += salary * 0.50m;

        if (isCandidate)
            allowances += salary * 0.50m;

        if (isDoctor)
            allowances += salary * 0.70m;

        if (isMethodLit)
            allowances += 100m;

        return allowances;
    }

    public static (decimal accrual, decimal ural, decimal tax, decimal toPay) CalculateAll(
        decimal salary,
        decimal allowances)
    {
        decimal accrual = salary + allowances;
        decimal ural = accrual * 0.15m;
        decimal tax = (accrual + ural) * 0.13m;
        decimal toPay = accrual + ural - tax;

        return (accrual, ural, tax, toPay);
    }
}
