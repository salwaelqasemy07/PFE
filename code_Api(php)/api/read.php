<?php
    $link = mysqli_connect('localhost', 'root', '','tracker') ;
    $response=array();
    if($link){
    $sql="select * from adresse";
    $result=mysqli_query($link,$sql);
    if ($result) {
        //header("Content-Type:JSON");
        $i=0;
        while ($row=mysqli_fetch_assoc($result)) {
            $response[$i]['id_adress']=$row['id_adress'];
            $response[$i]['adress']=$row['adress'];
            $response[$i]['latitude']=$row['latitude'];
            $response[$i]['longitude']=$row['longitude'];
            $response[$i]['date']=$row['date'];
            $i++;
            # code...
        }
        echo json_encode($response,JSON_PRETTY_PRINT);

    }

    }else{
        echo "data not connected";
    }
?>