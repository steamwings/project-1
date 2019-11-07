
function checkSubmit_New(){
    if(!hasVal("Name")){
        hide("divSubmit");
        return;
    };
    var type = document.getElementById("TypeId").value;
    switch (type) {
        case "1": // Checking
            if (hasVal("InterestId_Checking"))
                break;
        case "2": // Loan
            if(hasVal("Amount") && hasVal("InterestId_Loan"))
                break;
        case "3": // Investment
            if (hasVal("Amount") && hasVal("InterestId_Investment"))
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


function checkSubmit_WD() {
    if (!hasVal("Amount")) {
        hide("divSubmit");
        return;
    };
    show("divSubmit");
}

function updateFromType_New() {
    var type = document.getElementById("TypeId").value;
    switch (type) {
        case "1": // Checking
            hide("divAmount");
            hide("divLoan");
            hide("divTerm");
            show("divChecking");
            break;
        case "2": // Loan
            show("divAmount");
            show("divLoan");
            hide("divTerm");
            hide("divChecking");
            document.getElementById("labelAmount").innerHTML = "Loan Amount";
            document.getElementById("Amount").placeholder = "What is the size of the loan?";
            break;
        case "3": // Investment
            show("divAmount");
            hide("divLoan");
            show("divTerm");
            hide("divChecking");
            document.getElementById("labelAmount").innerHTML = "Term Deposit Amount";
            document.getElementById("Amount").placeholder = "How much would you like to invest?";
            break;
        default:
            hide("divAmount");
            hide("divLoan");
            hide("divTerm");
            hide("divChecking");
            break;
    }
}

function disableOption_Tx(selectModified, selectToModify){
    var val = selectedVal(selectModified);
    if(val == document.getElementById(selectToModify).value)
        document.getElementById(selectToModify).value = "";
}

function disableBtn_WD(btnModified, btnToModify) {
    show("divWDForm");
    document.getElementById(btnModified).disabled = true;
    document.getElementById(btnToModify).disabled = false;
    document.getElementById("divSubmit").value = btnModified;
    if(btnModified == "btnDeposit"){
        document.getElementById("formWD").action = "/Accounts/Deposit";
        document.getElementById("labelAmount").innerText = "Amount to Deposit";
        document.getElementById("btnSubmit").value = "Submit Deposit";
    } else if(btnModified == "btnWithdraw"){
        document.getElementById("formWD").action = "/Accounts/Withdraw";
        document.getElementById("labelAmount").innerText = "Amount to Withdraw";
        document.getElementById("btnSubmit").value = "Submit Withdrawal";
    }
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
