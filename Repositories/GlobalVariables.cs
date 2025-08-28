using InFeminine_Admin.Views.Articles;

namespace InFeminine_Admin.Repositories
{
    public static class GlobalVariables
    {
        private static Color backgroundColor = Colors.White;
        public static Color BackgroundColor { get { return backgroundColor; } set { backgroundColor = value; } }


        private static Dictionary<int, bool> pagesUsed = new()
        {
            [1] = false,
            [2] = false,
            [3] = false,
            [4] = false,
            [5] = false,
            [6] = false,
            [7] = false,
            [8] = false,
            [9] = false,
            [10] = false
        };
        public static Dictionary<int, bool> PagesUsed { get { return pagesUsed; } set { pagesUsed = value; } }


        private static List<ContentPage> pages =
        [
            new Article_01 { Title = "Pagina1" },
            new Article_02 { Title = "Pagina2" },
            new Article_03 { Title = "Pagina3" },
            new Article_04 { Title = "Pagina4" },
            new Article_05 { Title = "Pagina5" },
            new Article_06 { Title = "Pagina6" },
            new Article_07 { Title = "Pagina7" },
            new Article_08 { Title = "Pagina8" },
            new Article_09 { Title = "Pagina9" },
            new Article_10 { Title = "Pagina10" }
        ];
        public static List<ContentPage> Pages { get { return pages; } set { pages = value; } }


        private static Dictionary<int, string> pageTitles = new()
        {
            [1] = "Pagina1",
            [2] = "Pagina2",
            [3] = "Pagina3",
            [4] = "Pagina4",
            [5] = "Pagina5",
            [6] = "Pagina6",
            [7] = "Pagina7",
            [8] = "Pagina8",
            [9] = "Pagina9",
            [10] = "Pagina10"
        };
        public static Dictionary<int, string> PageTitles { get { return pageTitles; } set { pageTitles = value; } }
    }
}
