namespace Task6
{
    class FuzzySet
    {
        public string Name { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Peak { get; set; }

        public FuzzySet(string name, double min, double peak, double max)
        {
            Name = name;
            Min = min;
            Peak = peak;
            Max = max;
        }

        // Функція належності
        public double Membership(double value)
        {
            if (value <= Min || value >= Max)
                return 0.0;
            else if (value == Peak)
                return 1.0;
            else if (value > Min && value < Peak)
                return (value - Min) / (Peak - Min);
            else
                return (Max - value) / (Max - Peak);
        }
    }

}
