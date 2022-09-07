using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Business.Interface
{
    internal interface IColorBusiness
    {
        /// <summary>
        /// Set the color text of cursor and token during selection character by player index
        /// </summary>
        /// <param name="playerInputIndex">Player index in string</param>
        /// <returns>A Color by player index</returns>
        Color SetColorByPlayerIndex(string playerInputIndex);
    }
}
