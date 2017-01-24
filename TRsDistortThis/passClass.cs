namespace TRsDistortThis
{
    class passClass
    {
        static int thid;
        static string stid;
        public static int ThreadID
        {
            get
            {
                return thid;
            }
            set
            {
                thid = value;
            }
        }

        public static string StringID
        {
            get
            {
                return stid;
            }
            set
            {
                stid = value;
            }
        }
    }

}
