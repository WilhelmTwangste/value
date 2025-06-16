using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class MaterialCalculator
    {
        private readonly demo3Entities1 db = new demo3Entities1();
        public int CalculateMaterialRequired(int productTypeId, int materialTypeId, int productCount,
            double param1, double param2)
        {
            // Проверка на некорректные данные
            if (productCount <= 0 || param1 <= 0 || param2 <= 0)
                return -1;

            // Получаем данные из БД
            var productType = db.ProductType_.FirstOrDefault(pt => pt.IDProductType == productTypeId);
            var materialType = db.MaterialType_.FirstOrDefault(mt => mt.IDMaterialType == materialTypeId);

            if (productType == null || materialType == null || string.IsNullOrEmpty(productType.CoefficientProductType) || string.IsNullOrEmpty(materialType.PercentageDefectiveMaterial))
            {
                return -1;
            }

            // Парсим коэффициент и процент брака
            if (!double.TryParse(productType.CoefficientProductType, out double coefficient) || !double.TryParse(materialType.PercentageDefectiveMaterial, out double defectRate) || defectRate < 0)
            {
                return -1;
            }

            // Базовый расчёт: параметры * коэффициент * количество продукции
            double baseMaterial = param1 * param2 * coefficient * productCount;

            // Учитываем брак: увеличиваем на процент брака (переводим в долю)
            double totalMaterial = baseMaterial * (1 + defectRate / 100);

            // Округляем вверх до целого числа
            return (int)Math.Ceiling(totalMaterial);
        }
    }
}
