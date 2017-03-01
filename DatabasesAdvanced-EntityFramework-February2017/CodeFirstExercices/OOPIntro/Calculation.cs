namespace OOPIntro
{
    class Calculation
    {
        public const double PlanckConstant = 6.62606896e-34;
        public const double Pi = 3.14159;

        public static double GetReducedPlanckConstant()
        {
            return PlanckConstant / (2 * Pi);
        }
    }
}
