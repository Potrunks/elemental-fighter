using UnityEngine;

namespace Assets.Script.Business
{
    public interface IColorBusiness
    {
        /// <summary>
        /// Set the color text of cursor and token during selection character by player index
        /// </summary>
        /// <param name="playerInputIndex">Player index in string</param>
        /// <returns>A Color by player index</returns>
        Color SetColorByPlayerIndex(string playerInputIndex);
    }
}
