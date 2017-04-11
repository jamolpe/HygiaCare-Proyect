var express = require('express');
var bp = require('body-parser');
var app = express();

app.use(bp());
app.use(bp.json());

//var usuarios = require('./routes/Usuarios');
//app.use('/usuarios',usuarios);

var hospitales = require('./routes/Hospitales');
app.use('/hospitales',hospitales);

sql = require('mssql');
config = {
	user:'usurpador@servidorhygia',
	password:'el_molpe123',
	server:'servidorhygia.database.secure.windows.net',
	database: 'HygiaBD',
	options: {encrypt: true}
	
};

sql.connect(config, function (err) {
        if (err) console.log(err);
    });

app.listen(8000,function(){
	console.log('Marchando el servidor...')

})