var express = require('express');
var hospitales = express.Router();
module.exports = hospitales;


hospitales.get('/',function(req,res){

        // create Request object
        var request = new sql.Request();
           
        // query to the database and get the records
        request.query('select * from HOSPITAL', function (err, recordset) {
            
            if (err) console.log(err)

            // send records as a response
            res.status(200).send(recordset.recordset);
            
        });

});
