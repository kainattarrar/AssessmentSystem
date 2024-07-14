using AssessmentSystem.Service.Models.Enum;

namespace AssessmentSystem.Service.API.Services
{
    public class ScoreCalculationService
    {
        public static int LevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.IsNullOrEmpty(target) ? 0 : target.Length;
            }

            if (string.IsNullOrEmpty(target))
            {
                return source.Length;
            }

            int sourceLength = source.Length;
            int targetLength = target.Length;

            int[,] distance = new int[sourceLength + 1, targetLength + 1];

            // Initialize the distance matrix
            for (int i = 0; i <= sourceLength; i++)
            {
                distance[i, 0] = i;
            }

            for (int j = 0; j <= targetLength; j++)
            {
                distance[0, j] = j;
            }

            // Calculate the distances
            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(
                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceLength, targetLength];
        }

        public static double CalculateSimilarity(string source, string target)
        {
            int distance = LevenshteinDistance(source, target);
            int maxLength = Math.Max(source.Length, target.Length);

            // Calculate the similarity percentage
            return (1.0 - ((double)distance / maxLength)) * 100.0;
        }

        public int AssignScore(string source, string target, int points)
        {
            double similarity = CalculateSimilarity(source, target);

            if (similarity == 100)
            {
                return points;
            }
            else if (similarity >= 75)
            {
                return (int)(0.75 * points);
            }
            else if (similarity >= 50)
            {
                return (int)(0.50 * points);
            }
            else
            {
                return 0;
            }
        }
        public Rank CalculateRank(int candidateScore, int totalScore)
        {
            if (totalScore == 0)
            {
                throw new ArgumentException("Total score cannot be zero.", nameof(totalScore));
            }

            double percentage = ((double)candidateScore / totalScore) * 100;

            if (percentage > 80)
            {
                return Rank.A;
            }
            else if (percentage >= 70)
            {
                return Rank.B;
            }
            else if (percentage >= 60)
            {
                return Rank.C;
            }
            else
            {
                return Rank.F;
            }
        }
    }
}
