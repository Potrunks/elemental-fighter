﻿using UnityEngine;

namespace Assets.Script.Business
{
    public class ColorBusiness : IColorBusiness
    {
        public Color SetColorByPlayerIndex(string playerInputIndex)
        {
            switch (playerInputIndex)
            {
                case "2":
                    return Color.blue;

                case "3":
                    return Color.green;

                case "4":
                    return Color.yellow;

                default:
                    return Color.red;
            }
        }
    }
}
