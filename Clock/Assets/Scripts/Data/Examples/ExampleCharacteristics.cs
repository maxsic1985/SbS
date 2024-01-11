namespace MSuhinin.Clock
{
    [System.Serializable]
    public sealed class ExampleCharacteristics
    {
        public string Name;
        public int StaffCountForLittleShip=5;
        public int StaffCountForMiddleShip=10;
        public int StaffCountForLargeShip=20;
        
        
        public float GetStuffCount(TestEnumType type)
        {
            var result = 0.0f;
            switch (type)
            {
                case TestEnumType.None:
                    break;
                case TestEnumType.Little:
                    result = StaffCountForLittleShip;
                    break;
                case TestEnumType.Midle:
                    result = StaffCountForMiddleShip;
                    break;
                case TestEnumType.Large:
                    result = StaffCountForLargeShip;
                    break;
                default:
                    break;
            }

            return result;
        }
        
    }
}