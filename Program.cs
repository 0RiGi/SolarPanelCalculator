namespace SolarPanelCalculator
{
    class SolarPanelSpec
    {
        public SolarPanelSpec(
            double widthMM,
            double heightMM,
            double thicknessMM,
            double weightGrams,
            double ratingKWh,
            double efficiencyPercent,
            double costPounds,
            int lifeExpectancyYears,
            string manufacturer)
        {
            widthMM_ = widthMM;
            heightMM_ = heightMM;
            thicknessMM_ = thicknessMM;
            weightGrams_ = weightGrams;


            ratingKWh_ = ratingKWh;
            efficiencyPercent_ = efficiencyPercent;
            costPounds_ = costPounds;

            lifeExpectancyYears_ = lifeExpectancyYears;
            manufacturer_ = manufacturer;
        }

        public double getSizeM2()
        {
            return (widthMM_ / 1000) * (heightMM_ / 1000);
        }

        public double getCost()
        {
            return costPounds_;
        }

        public string getManufacaturer()
        {
            return manufacturer_;
        }

        public double getWeight()
        {
            return weightGrams_;
        }


        private double widthMM_;
        private double heightMM_;
        private double thicknessMM_;
        private double weightGrams_;

        private double ratingKWh_;
        private double efficiencyPercent_;
        private double costPounds_;

        private int lifeExpectancyYears_;
        private string manufacturer_;
    }


    class Calculator
    {
        public Calculator()
        {
            roofSizeM2_ = 10;
            orientationDegrees_ = 0;
            budgetPounds_ = 10000;
            yearlyConsumptionKWh_ = 100;
            peakConsumptionMonthlyKWh_ = 15;
        }
        public void setSolarPanelSpec(SolarPanelSpec sps)
        {
            sps_ = sps;

        }
        public int getNumberOfSolarPanels()
        {
            double solarPanelSizeM2 = sps_.getSizeM2();
            const double roofSlackPercent = 10;

            double effectiveRoofSize = roofSizeM2_ - roofSizeM2_ * (roofSlackPercent / 100);

            double numberOfSolarPanels = effectiveRoofSize / solarPanelSizeM2;

            int effectiveNumberOfPanels = (int)(System.Math.Floor(numberOfSolarPanels));

            //System.Console.WriteLine("panel size: " + solarPanelSizeM2);
            //System.Console.WriteLine("roof size: " + effectiveRoofSize);
            //System.Console.WriteLine("number of panels: " + effectiveNumberOfPanels);
            return effectiveNumberOfPanels;
        }

        private double calculateCost()
        {
            double solarPanelCostPounds = sps_.getCost();

            return getNumberOfSolarPanels() * solarPanelCostPounds;
        }

        public bool isWithinBudget()
        {
            return calculateCost() < budgetPounds_;
        }

        public double ExtraSolarPanlesTheClientCouldAdd()
        {
            if (howMuchAwayFromBudget() > sps_.getCost())
                return System.Math.Floor(extraSolarPanels = howMuchAwayFromBudget() / sps_.getCost());
            else
                return 0;
        }

        public double howMuchAwayFromBudget()
        {
            return budgetPounds_ - calculateCost();
        }

        public double maxNumberOfSolarPanelsWithinBudget()
        {
            if (isWithinBudget()) 
                return getNumberOfSolarPanels();
            else
                return System.Math.Floor(getNumberOfSolarPanels() - System.Math.Abs(howMuchAwayFromBudget()) / sps_.getCost());
        }

        private double totalSolarPanelWeight()
        {
            return maxNumberOfSolarPanelsWithinBudget() * sps_.getWeight();
        }
        
        public void printResults()
        {
            System.Console.WriteLine("the manufacturer of the solar panel is: " + sps_.getManufacaturer() +" and the roof size is: " + roofSizeM2_);
            System.Console.WriteLine("required solar panels: " + getNumberOfSolarPanels());
            System.Console.WriteLine("is within budget: " + isWithinBudget());
            System.Console.WriteLine("over budget by: " + howMuchAwayFromBudget());
            System.Console.WriteLine("number of solar panels within budget: " + maxNumberOfSolarPanelsWithinBudget());
            System.Console.WriteLine("you could also add this number of soalr panels: " + ExtraSolarPanlesTheClientCouldAdd());
            System.Console.WriteLine("the total weight of the solar panels that are within budget is: " + totalSolarPanelWeight() + "g");
            System.Console.WriteLine();
        }


        public double extraSolarPanels;
        public int roofSizeM2_;
        public int orientationDegrees_;
        public int budgetPounds_;
        public int yearlyConsumptionKWh_;
        public int peakConsumptionMonthlyKWh_; // Winter usually

        private SolarPanelSpec sps_;
    }

    class Program
    {
        static void Main(string[] args)
        {
            SolarPanelSpec PremiumSP = new SolarPanelSpec(350, 2100, 15, 5, 8, 15, 314.99, 25, "tesla");
            SolarPanelSpec CheapSP = new SolarPanelSpec(300, 2000, 20, 7, 5, 13, 200, 15, "aliExpress");

            Calculator c = new Calculator();
            c.roofSizeM2_ = 50;

            c.setSolarPanelSpec(PremiumSP);
            c.printResults();

            c.setSolarPanelSpec(CheapSP);
            c.printResults(); 
            
            
            c.roofSizeM2_ = 20;

            c.setSolarPanelSpec(PremiumSP);
            c.printResults();

            c.setSolarPanelSpec(CheapSP);
            c.printResults();
        }
    }
}