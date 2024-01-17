const tblName="Tbl_Name";
$('#btnSave').click(function(){
  if(_editId==null){
    createData();
  }
  else{
    updateData();
  }
   
})

function createData(){

  let lst=[];
  //var aa= localStorage.getItem(tblName);
 // console.log(aa);
   if(localStorage.getItem(tblName)!=null){
       lst=JSON.parse(localStorage.getItem(tblName));
       console.log({lst}) 
     }
   const name=$('#txtname').val();
   const data={
       id: uuidv4(),
       Name: name
   };
   lst.push(data);
   console.log({lst});
   localStorage.setItem(tblName,JSON.stringify(lst));

   //alert("Saving Successful");
  successMessage("Saving Successful")
   $('#txtname').val('');
   $('#txtname').focus();
   readData();
}

function readData(){
  if(localStorage.getItem(tblName)==null)return;

  var Jsonstr=localStorage.getItem(tblName);
  var lst=JSON.parse(Jsonstr);
  let trRows='';
  let count=0;
  lst.forEach(item => {
    //console.log({item});
    trRows += `
    <tr>
      <td>
      <button type="button" class="btn btn-warning" onclick="editData('${item.id}')">
      <i class="fa-solid fa-pen-to-square"></i></button>
      <button type="button" class="btn btn-danger" onclick="deleteData('${item.id}')">
      <i class="fa-solid fa-trash"></i></button>
      </td>
      <td>${++count}</td>
      <td>${item.Name}</td>
    </tr>
    `
  });

 // console.log(trRows);
  $('#tableData').html(trRows);
}
function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
      (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
  }

  let _editId=null;
  function editData(id){
    if(localStorage.getItem(tblName)==null) return;

    var Jsonstr=localStorage.getItem(tblName);
    var lst=JSON.parse(Jsonstr);

    var results= lst.filter(x =>x.id ==id) ;
    //console.log(results);
        if(results.length == 0){
          alert('No data Found')
          return ;
        }
    var item = results[0];
    _editId=item.id;
    $('#txtname').val(item.Name);
  //  console.log(_editId);
  }

  function  updateData(){
    let lst=[];
     if(localStorage.getItem(tblName)!=null){
         lst=JSON.parse(localStorage.getItem(tblName));
         console.log({lst}) 
       }

       let index=lst.findIndex(x=>x.id == _editId);
      /// console.log(index);
       lst[index].Name=$('#txtname').val();
    
     console.log({lst});
     localStorage.setItem(tblName,JSON.stringify(lst));
     alert("updating successful");
     readData();
  }

  function deleteData(id){
    //let result=confirm("Are you sure want to delete?.");
    confirmMessage('Are you sure want to delete?').then(function(result){

      if(!result) return;
      let lst=[];
      if(localStorage.getItem(tblName)!=null){
      lst=JSON.parse(localStorage.getItem(tblName));
      console.log({lst}) ;
      }
      lst=lst.filter(x=>x.id != id);
      localStorage.setItem(tblName,JSON.stringify(lst));
      successMessage("Deleting successful");
     readData();
    
    })
        
      }
   // });
    //if(!result) return;
   
 // }
  readData();