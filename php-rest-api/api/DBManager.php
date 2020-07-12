<?php

    class DBManager{
        const USER = "root";
        const HOST = "127.0.0.1";
        const DATABASE = "phpapi_asp";

        private static $PDO;

        public static function getConnection() :?object
        {
            try {
                if(self::$PDO == null) self::$PDO = new PDO("mysql:host=".self::HOST.";dbname=".self::DATABASE,self::USER);
            } catch (PDOException $e) {
                print "Erreur !: " . $e->getMessage() . "<br/>";
                self::$PDO = null;
            }
            return self::$PDO;
        }
    }
?>