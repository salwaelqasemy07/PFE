
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:tracker/second_page.dart';
import 'package:tracker/third_page.dart';

void main() {
  runApp(MyApp());
}
class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      theme: ThemeData(primaryColor: Colors.blue),
      debugShowCheckedModeBanner: false,
      home: MyHomePage(),
    );
  }
}
class MyHomePage extends StatefulWidget {
  @override
  _MyHomePageState createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Color.fromRGBO(13, 7, 19, 1),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Padding(padding: EdgeInsets.only(top: 200.0)),
            Image.asset(
              "assets/focus (1).png",
              scale: 0.7,
            ),
            Padding(padding: EdgeInsets.only(top: 15.0)),
            Text(
              "Tracker",
              style: TextStyle(
                  fontSize: 28.0,
                  color: Colors.white,
                  fontFamily: "Adobe Fangsong Std"),
            ),
            Padding(padding: EdgeInsets.only(top: 5.0)),
            Text(
              "The best app to track someone",
              style: TextStyle(color: Colors.grey, fontSize: 10.0),
            ),
            Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                Padding(padding: EdgeInsets.only(bottom: 180.0)),
                FlatButton(
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(20),
                        side: BorderSide(color: Colors.grey)),
                    color: Colors.grey,
                    onPressed: () {
                      Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => FirstPage()));
                    },
                    child: Text(
                      "Start",
                      style: TextStyle(color: Color.fromRGBO(13, 7, 19, 1)),
                    ))
              ],
            )
          ],
        ),
      ),
    );
  }
}
class FirstPage extends StatefulWidget {
  @override
  FirstPageState createState() => FirstPageState();
}
class FirstPageState extends State<FirstPage> {
   int _seletedItem = 0;
  var _pages = [SecondPage(), ThirdPage()];
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Location"),
      ),
      body: Center(child: _pages[_seletedItem],),
       bottomNavigationBar: BottomNavigationBar(
        items: <BottomNavigationBarItem>[
          BottomNavigationBarItem(icon: Icon(Icons.home), title: Text('Home')),
          BottomNavigationBarItem(icon: Icon(Icons.history), title: Text('History')),
        ],
        currentIndex: _seletedItem,
        onTap: (index){
          setState(() {
            _seletedItem = index;
          });
        },
      ),
    );
  }
}

