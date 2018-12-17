using System.Collections;
using Character;
using UI;

namespace CenterSystem
{
    public static class DataConnector
    {
        public static ArrayList DashBoardList;
        public static State PlayerState;

        public static void RegisterDashBoard(IDashBoard d)
        {
            DashBoardList.Add(d);
        }
    }
}