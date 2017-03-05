namespace Gringotts.UI
{
    using System;

    public class Startup
    {
        static void Main()
        {
            #region // Tag Transformer
            var tagToTransform = Console.ReadLine();
            var resultTag = TagTransformer.Transform(tagToTransform);
            Console.WriteLine($"{resultTag} was added to database");
            #endregion
        }
    }
}
