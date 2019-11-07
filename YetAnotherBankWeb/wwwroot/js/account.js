
function updateFromType_New(){
    // document.getElementById("InterestId_Checking").value = "";
    // document.getElementById("InterestId_Investment").value = "";
    // document.getElementById("InterestId_Loan").value = "";
    var type = document.getElementById("TypeId").value;
    switch(type){
        case "2": // Loan
            show("divLoan");
            hide("divTerm");
            hide("divChecking");
            break;
        case "3": // Investment
            hide("divLoan");
            show("divTerm");
            hide("divChecking");
            break;
        case "1": // Checking
            hide("divLoan");
            hide("divTerm");
            show("divChecking");
            break;
        default:
            hide("divLoan");
            hide("divTerm");
            hide("divChecking");
    }
}

function checkSubmit_New(){
    if(!hasVal("Name")){
        hide("divSubmit");
        return;
    };
    var type = document.getElementById("TypeId").value;
    switch (type) {
        case "1": // Checking
            if(hasVal("InterestId_Checking"))
                break;
        case "2": // Loan
            if(hasVal("LoanAmount") && hasVal("InterestId_Loan"))
                break;
        case "3": // Investment
            if(hasVal("InterestId_Investment"))
                break;
        default:
            hide("divSubmit");
            return;
    }
    show("divSubmit");
}

function checkSubmit_Tx() {
    if (!hasVal("Amount") || !hasVal("selectInto") || !hasVal("selectOutOf") ) {
        hide("divSubmit");
        return;
    };
    show("divSubmit");
}

function disableOption_Tx(selectModified, selectToModify){
    var val = selectedVal(selectModified);
    if(val == document.getElementById(selectToModify).value)
        document.getElementById(selectToModify).value = "";

    // var op = document.getElementById(selectToModify).getElementsByTagName("option");

    // for (var i = 0; i < op.length; i++) {
    //     (op[i].value == val)
    //         ? op[i].disabled = true
    //         : op[i].disabled = false;
    // }
}

// -------------- Helper functions --------------

function selectedVal(selectName){
    var op = document.getElementById(selectName).getElementsByTagName("option");
    for (var i = 0; i < op.length; i++) {
        if(op[i].selected)
            return op[i].value;
    }
}

function hello() { alert("Hello!"); }

function show(id) {
    // alert("Hello!");
    document.getElementById(id).style.display = "block";
}
function hide(id) {
    document.getElementById(id).style.display = "none";
}
function hasVal(id) {
    var v = document.getElementById(id).value;
    return !(v === undefined || v === "");
}
