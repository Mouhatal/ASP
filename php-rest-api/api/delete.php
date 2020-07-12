<?php
    header("Access-Control-Allow-Origin: *");
    header("Content-Type: application/json; charset=UTF-8");
    header("Access-Control-Allow-Methods: POST");
    header("Access-Control-Max-Age: 3600");
    header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

    include_once './DBManager.php';
    include_once './personne.php';


    $item = new Personne();

    $data = json_decode(file_get_contents("php://input"));

    //$item->IdPersonne = $data->IdPersonne;

    if($item->delete($data->IdPersonne)){
        echo json_encode("Personne deleted.");
    } else{
        echo json_encode("Data could not be deleted");
    }
?>