function successMessage(message){
   // Swal.fire({
     //title: "Success",
       // text: message,
       // icon: "success"

     // });
     
     Notiflix.Report.success(
        'Success',
        message,
        'Okay',
        );
}

function confirmMessage(message){
     return new Promise(function(myResolve, myReject) {
         Swal.fire({
             title: "Confirm",
             text: "Are you sure want to delete?.",
             icon: "warning",
             showCancelButton: true,
             confirmButtonColor: "Yes",
            
           }).then((result) => {
             myResolve( result.isConfirmed); 
           });

        // Notiflix.Confirm.show(
        //   'Confirm',
        //   message,
        //   'Yes',
        //   'No',
        //   function okCb() {
        //   myResolve(true);
        //   },
        //   function cancelCb() {
        //   myResolve(false);
        //   }
        //  );
         });
}