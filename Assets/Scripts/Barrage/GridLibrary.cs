using System.Collections.Generic;

namespace Barrage
{
    public static class GridLibrary
    {
        public static List<int[,]> TypeList;

        
        public static int[,] Get(int serial)
        {
            switch (serial)
            {
                case 0:
                    {
                        int[,] a = new int[5, 3]
                        {
                        {0,0,1},
                        {0,1,0},
                        {1,0,0},
                        {0,1,0},
                        {0,0,1}
                        };
                        return a;
                    }
                case 1:
                    {
                        int[,] a = new int[2, 1]
                        {
                        {1},
                        {1}
                        };
                        return a;
                    }
                case 2:
                    {
                        int[,] a = new int[4, 4]
                        {
                        {1,0,0,0},
                        {0,1,0,0},
                        {0,0,2,0},
                        {0,0,0,2}
                        };
                        return a;
                    }
                case 3:
                    {
                        int[,] a = new int[4, 4]
                        {
                        {0,0,0,2},
                        {0,0,2,0},
                        {0,1,0,0},
                        {1,0,0,0}
                        };
                        return a;
                    }

                case 4:
                    {
                        int[,] a = new int[3, 1]
                        {
                        {1},
                        {1},
                        {1}
                        };
                        return a;
                    }
                case 5:
                    {
                        int[,] a = new int[2, 4]
                        {
                        {1,1,1,1},
                        {1,1,1,1},
                        };
                        return a;
                    }
            }

            return null;
        }
    }
}