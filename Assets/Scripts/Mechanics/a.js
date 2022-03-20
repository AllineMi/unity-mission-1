let allEntries = document.querySelectorAll("._1S0_Sr9wFW3Nn5B-jus8Eo")[0].querySelectorAll("._1O7iJ28kSzxIqmyd2OFqCp");

function RemoveEntry(x) {
    allEntries[x].remove();
}

for(let x = 0; x < allEntries.length; x++){
    // get email text
    let text = allEntries[x].querySelector("._3rw0Dcxy1cUZZzm4rdKTVi").textContent;
    // domains to check for
    let toHide = ["gmail.com", "terra.com.br", "outlook.com", "hotmail.com"];
    // check for arroba
    if(!text.includes("@")){
        RemoveEntry(x);
    }

    if(text.includes("@") && toHide.indexOf(text)){
        RemoveEntry(x);        
    }
}