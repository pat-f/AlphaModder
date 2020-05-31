using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaModder.Constants
{
    class AlphaModderConstants
    {
        public const String VERSION = "1.1.0-beta";

        public const String DIRECTDRAW_1_LOW_RES_LINE = "DirectDraw=1";
        public const String DIRECTDRAW_0_HIGH_RES_LINE = "DirectDraw=0";

        public const String RETOOL_STRICTNESS_0_ALWAYS_FREE = "0";
        public const String RETOOL_STRICTNESS_1_FREE_IN_CATEGORY = "1";
        public const String RETOOL_STRICTNESS_2_FREE_IF_PROJECT = "2";
        public const String RETOOL_STRICTNESS_3_NEVER_FREE = "3";

        public const String TRIAD_0_LAND = "0";
        public const String TRIAD_1_SEA = "1";
        public const String TRIAD_2_AIR = "2";

        public const String ERROR_GAME_FOLDER_NOT_FOUND = "No game folder was found.  Please configure it in the Tools menu.";

        public const String APP_DATA_RELATIVE_PATH = "/AlphaModderData/";
        public const String ALPHA_FILENAME = "alpha.txt";
        public const String ALPHA_X_FILENAME = "alphax.txt";
        public const String ALPHA_CENTAURI_INI_FILENAME = "Alpha Centauri.Ini";
    }
}
