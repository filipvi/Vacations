﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Vacations.Utilities.Navigation
{
    public static class NavigationModel
    {
        private const string Underscore = "_";
        private const string Dash = "-";
        private const string Space = " ";
        private static readonly string Empty = string.Empty;

        public static string NavigationJsonFile { get; set; }
        //public static bool UserAuthenticated { get; set; }

        public static SmartNavigation Seed => BuildNavigation();
        public static SmartNavigation Full => BuildNavigation(seedOnly: false);

        private static SmartNavigation BuildNavigation(bool seedOnly = true)
        {

            var jsonText = File.ReadAllText(NavigationJsonFile);
            var navigation = NavigationBuilder.FromJson(jsonText);
            var menu = FillProperties(navigation.Lists, seedOnly);

            return new SmartNavigation(menu);
        }

        private static List<ListItem> FillProperties(IEnumerable<ListItem> items, bool seedOnly, ListItem parent = null)
        {
            var result = new List<ListItem>();

            foreach (var item in items)
            {
                item.Text ??= item.Title;
                item.Tags = string.Concat(parent?.Tags, Space, item.Title.ToLower()).Trim();

                var parentRoute = (Path.GetFileNameWithoutExtension(parent?.Text ?? Empty)?.Replace(Space, Underscore) ?? Empty).ToLower();
                var sanitizedHref = parent == null ? item.Href?.Replace(Dash, Empty) : item.Href?.Replace(parentRoute, parentRoute.Replace(Underscore, Empty)).Replace(Dash, Empty);
                var route = Path.GetFileNameWithoutExtension(sanitizedHref ?? Empty)?.Split(Underscore) ?? Array.Empty<string>();

                item.Route = route.Length > 1 ? $"/{route.First()}/{string.Join(Empty, route.Skip(1))}" : item.Href;

                item.I18n = parent == null
                    ? $"nav.{item.Title.ToLower().Replace(Space, Underscore)}"
                    : $"{parent.I18n}_{item.Title.ToLower().Replace(Space, Underscore)}";
                item.Type = parent == null ? item.Href == null ? ItemType.Category : ItemType.Single : item.Items.Any() ? ItemType.Parent : ItemType.Child;
                item.Items = FillProperties(item.Items, seedOnly, item);

                if (!seedOnly || item.ShowOnSeed)
                    result.Add(item);
            }

            return result;
        }
    }

    #region Old navigation

    //public static class NavigationModel
    //{
    //    private const string Underscore = "_";
    //    private const string Space = " ";

    //    public static SmartNavigation Seed => BuildNavigation();
    //    public static SmartNavigation SeedAnonymous => BuildNavigationAnonymous();

    //    private static SmartNavigation BuildNavigation(bool seedOnly = true)
    //    {

    //        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    //        //Encoding encoding = Encoding.GetEncoding("iso-8859-2");
    //        Encoding encoding = Encoding.Unicode;

    //        var jsonText = File.ReadAllText("nav.json", encoding);
    //        var navigation = NavigationBuilder.FromJson(jsonText);
    //        var menu = FillProperties(navigation.Lists, seedOnly);

    //        return new SmartNavigation(menu);
    //    }

    //    private static SmartNavigation BuildNavigationAnonymous(bool seedOnly = true)
    //    {

    //        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    //        //Encoding encoding = Encoding.GetEncoding("iso-8859-2");

    //        var jsonText = File.ReadAllText("nav_anonymous.json", Encoding.Unicode);
    //        var navigation = NavigationBuilder.FromJson(jsonText);
    //        var menu = FillProperties(navigation.Lists, seedOnly);

    //        return new SmartNavigation(menu);
    //    }

    //    private static List<ListItem> FillProperties(IEnumerable<ListItem> items, bool seedOnly, ListItem parent = null)
    //    {
    //        var result = new List<ListItem>();

    //        foreach (var item in items)
    //        {
    //            item.Text = item.Text ?? item.Title;
    //            item.Tags = string.Concat(parent?.Tags, Space, item.Title.ToLower()).Trim();

    //            // ReSharper disable once ConstantConditionalAccessQualifier
    //            var route = Path.GetFileNameWithoutExtension(item.Href ?? string.Empty)?.Split(Underscore);

    //            // ReSharper disable once ConstantConditionalAccessQualifier
    //            item.Route = route?.Length > 1 ? $"/{route.First()}/{string.Join(string.Empty, route.Skip(1))}" : item.Href;

    //            item.I18N = parent == null
    //                ? $"nav.{item.Title.ToLower().Replace(Space, Underscore)}"
    //                : $"{parent.I18N}_{item.Title.ToLower().Replace(Space, Underscore)}";

    //            item.Items = FillProperties(item.Items, seedOnly, item);

    //            if (!seedOnly || item.ShowOnSeed)
    //                result.Add(item);
    //        }

    //        return result;
    //    }
    //}

    #endregion
}
