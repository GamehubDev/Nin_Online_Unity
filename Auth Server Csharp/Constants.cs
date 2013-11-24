using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthServer
{
    static class Constants
    {

        //Database
        public const string DB_SERVER = "Server=mysql.ninonline.org;";
        public const string DB_PORT = "Port=3306;";
        public const string DB_NAME = "Database = ninonline;";
        public const string DB_UID = "UID = rorysoh;";
        public const string DB_PWD = "pwd = 6qR4Jks1;";
        public const int MAX_RETRIES = 5;

        //Globals
        public const int MAX_SERVERS = 1;
        public const int MAX_CLIENTS_AT_ONCE = 50; //originally MAX_PLAYER_CONNECTION

        //Player
        public const int PLAYER_NAME_LENGTH = 20;
        public const int MAX_INV = 40;
        public const int MAX_PLAYER_SPELLS = 40;
        public const int MAX_HOTBAR = 10;
        public const int MAX_SWITCHES = 201;
        public const int MAX_VARIABLES = 201;
        public const int MAX_PLAYER_QUESTS = 11;
        public const int MAX_QUESTS = 100;
        public const int MAX_PLAYER_FRIENDS = 30;
        public const int MAX_ELEMENTS = 5;
        public const int MAX_BANK = 99;
        //Login

        public const int ACCESS_FREE = 0;
        public const int ACCESS_DONOR = 1;
        public const int ACCESS_MAPPER_DEVELOPER = 2;
        public const int ACCESS_GAMEMASTER = 3;
        public const int ACCESS_ADMIN = 4;
        public const string salt = "oA1WGYScnyPMgCIIKmE8ivPdlFx9mabeHIpvf5XkZQW5emmgIeZnw5irTAUyGcx4Ehsc2QaCGp7zL3neieXNH2Vt54OP7zmAe3jTScyEZdXXxFuHRu9IBpjjzoellA8S0WbRxHPRo4QsW7HDPpne94X16ilqFAXMNrb05N1QLlFlcWvNWuLIfSS7tyJ2PYD2XSTADAbvIAIADRqyzRn1Zkw6tgAUcAKI5JPkErZBUnjF0ePm0OFPa4s2kdHVcdl7Ufi";


    }
}
