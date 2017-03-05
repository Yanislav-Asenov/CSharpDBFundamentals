using System.Text;

namespace Gringotts.UI
{
    public static class TagTransformer
    {
        public static string Transform(string tag)
        {
            StringBuilder resultTag = new StringBuilder();
            int tagMaxLength = 20;

            if (tag[0] != '#')
            {
                resultTag.Append("#");
                tagMaxLength--;
            }

            int resultTagIndex = 0;
            int inputTagIndex = 0;
            while (resultTagIndex < tagMaxLength && inputTagIndex < tag.Length)
            {
                if (tag[inputTagIndex] != ' ')
                {
                    resultTag.Append(tag[inputTagIndex]);
                    resultTagIndex++;
                }

                inputTagIndex++;
            }

            return resultTag.ToString();
        }
    }
}
