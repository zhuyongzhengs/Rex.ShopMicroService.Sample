namespace Rex.Service.Core.Helper
{
    /// <summary>
    /// 商品规格sku计算
    /// </summary>
    public class SkuHelper
    {
        public static string[] Process(string[][] sku)
        {
            int len = sku.Length;
            if (len >= 2)
            {
                var s1 = sku[0];
                int len1 = s1.Length;
                var s2 = sku[1];
                int len2 = s2.Length;

                int len3 = len1 * len2;
                int index = 0;
                string[] items = new string[len3];
                for (int i = 0; i < len1; i++)
                {
                    for (int j = 0; j < len2; j++)
                    {
                        items[index] = sku[0][i] + "," + sku[1][j];
                        index++;
                    }
                }
                // 将新组合的数组并到原数组中
                string[][] newArr = new string[len - 1][];
                for (int i = 2; i < sku.Length; i++)
                {
                    newArr[i - 1] = sku[i];
                }
                newArr[0] = items;
                return Process(newArr);
            }
            else
            {
                return (string[])sku[0];
            }
        }
    }
}