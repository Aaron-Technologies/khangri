// ****************** total cost position fixed ******************

    const fixedElement = document.querySelector('.booking_cost');
    const footer = document.querySelector('.subscriber-area');
    const header = document.querySelector('.header-area');
    const headerHeight = header.offsetHeight;

    window.addEventListener('scroll', () => {
      const footerTop = footer.getBoundingClientRect().top;
      if (window.scrollY > 350 && footerTop > fixedElement.offsetHeight) {
        fixedElement.classList.add('fixed');
      } else {
        fixedElement.classList.remove('fixed');
      }
    });
 


// ****************** booking cost slider arrow ⬅️ ******************
document.getElementById('toggleBtn').addEventListener('click', function() {
  // const box = document.querySelector('.booking_cost');
  const btn = this;

  fixedElement.classList.toggle('active');

  // Change arrow direction
  if (fixedElement.classList.contains('active')) {
    btn.textContent = '➡'; // show left arrow
  } else {
    btn.textContent = '⬅'; // show right arrow
  }
});



// Silk Route Packages in Ready Made 
function selectItem(name, imageUrl) {
  const dropdownButton = document.getElementById('dropdownMenuButton');
  dropdownButton.innerHTML = `
    <img src="${imageUrl}" alt="${name}">
    ${name}
  `;

  // Show or hide the div based on the selected option
  const hiddenDiv = document.getElementById('hiddenDiv');
  if (name === 'Silk Route Packages') {
    hiddenDiv.style.display = 'none';
  } else {
    hiddenDiv.style.display = 'block';
  }
}


// *****************************set Date range ******************************
$(function () {
const minValue = 3; // Minimum value
const maxValue = 21; // Maximum value
const defaultValue = 8; // Default selected value

// Initialize slider
$("#date-range1").slider({
  range: "min", // Single handle slider
  min: minValue, // Set minimum value
  max: maxValue, // Set maximum value
  value: defaultValue, // Set default value
  slide: function (event, ui) {
    // Update input with dynamic value
    $("#date-num1").val("" + ui.value);
  },
});

// Set initial value
$("#date-num1").val(`${defaultValue}`);
});



$(function () {
const minValue = 3; // Minimum value
const maxValue = 21; // Maximum value
const defaultValue = 8; // Default selected value

// Initialize slider
$("#date-range2").slider({
  range: "min", // Single handle slider
  min: minValue, // Set minimum value
  max: maxValue, // Set maximum value
  value: defaultValue, // Set default value
  slide: function (event, ui) {
    // Update input with dynamic value
    $("#date-num2").val("" + ui.value);
  },
});

// Set initial value
$("#date-num2").val(`${defaultValue}`);
});



$(function () {
const minValue = 3; // Minimum value
const maxValue = 21; // Maximum value
const defaultValue = 8; // Default selected value

// Initialize slider
$("#date-range3").slider({
  range: "min", // Single handle slider
  min: minValue, // Set minimum value
  max: maxValue, // Set maximum value
  value: defaultValue, // Set default value
  slide: function (event, ui) {
    // Update input with dynamic value
    $("#date-num3").val("" + ui.value);
  },
});

// Set initial value
$("#date-num3").val(`${defaultValue}`);
});



$(function () {
const minValue = 3; // Minimum value
const maxValue = 21; // Maximum value
const defaultValue = 8; // Default selected value

// Initialize slider
$("#date-range5").slider({
  range: "min", // Single handle slider
  min: minValue, // Set minimum value
  max: maxValue, // Set maximum value
  value: defaultValue, // Set default value
  slide: function (event, ui) {
    // Update input with dynamic value
    $("#date-num5").val("" + ui.value);
  },
});

// Set initial value
$("#date-num5").val(`${defaultValue}`);
});



$(function () {
const minValue = 1; // Minimum value
const maxValue = 19; // Maximum value
const defaultValue = 6; // Default selected value

// Initialize slider
$("#date-range6").slider({
  range: "min", // Single handle slider
  min: minValue, // Set minimum value
  max: maxValue, // Set maximum valuedate-num6
  value: defaultValue, // Set default value
  slide: function (event, ui) {
    // Update input with dynamic value
    $("#date-num6").val("" + ui.value);
  },
});

// Set initial value
$("#date-num6").val(`${defaultValue}`);
});


$(function () {
const minValue = 1; // Minimum value
const maxValue = 2; // Maximum value
const defaultValue = 1; // Default selected value

// Initialize slider
$("#date-range7").slider({
  range: "min", // Single handle slider
  min: minValue, // Set minimum value
  max: maxValue, // Set maximum value
  value: defaultValue, // Set default value
  slide: function (event, ui) {
    // Update input with dynamic value
    $("#date-num7").val("" + ui.value);
  },
});

// Set initial value
$("#date-num7").val(`${defaultValue}`);
});



// *****************************set price range ******************************
$(function () {
const minValue = 10000; // Minimum value
const maxValue = 80000; // Maximum value
const defaultValue = 20000; // Default selected value

// Initialize slider
$("#price-range4").slider({
  range: "min", // Single handle slider
  min: minValue, // Set minimum value
  max: maxValue, // Set maximum value
  value: defaultValue, // Set default value
  slide: function (event, ui) {
    // Update input with dynamic value
    $("#price-num4").val("" + ui.value);
  },
});

// Set initial value
$("#price-num4").val(`${defaultValue}`);
});




//  *********************hotel booking form*****************  

document.addEventListener("DOMContentLoaded", function () {
  const divCount = document.getElementById("divCount");
  const dynamicContent = document.getElementById("dynamicContent");
  const totalDisplay = document.getElementById("totalDisplay");

  // Function to create dynamic divs
  function createDynamicDivs(count) {
    // Clear existing dynamic content
    dynamicContent.innerHTML = "";

    for (let i = 1; i <= count; i++) {
      const newDiv = document.createElement("div");
      newDiv.className = "dynamic-div";

      newDiv.innerHTML = `
      <div class="row room_list_single bg_light_t">
        <div class="col-lg-3 d-flex align-items-center">
          <h4 class="text-primary">Room ${i}</h4>
        </div>
        <div class="col-lg-3">
          <label class="label-text">Adults <span class="text-primary">(Age 13+)</span></label>
          <select class="adultCount form-control">
            <option value="1">1</option>
            <option value="2" selected>2</option>
            <option value="3">3</option>
          </select>
        </div>
        <div class="col-lg-3">
          <label class="label-text">Child <span class="text-primary">(5-12 years)</span></label>
          <select class="childCount form-control"></select>
        </div>
        <div class="col-lg-3">
          <label class="label-text">Infant <span class="text-primary">(below 5 years)</span></label>
          <select class="infantNo form-control">
            <option value="0" selected>0</option>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
          </select>
        </div>
      </div>
    `;

      dynamicContent.appendChild(newDiv);

      // Initialize dropdowns
      const adultSelect = newDiv.querySelector(".adultCount");
      const childSelect = newDiv.querySelector(".childCount");

      const updateChildOptions = () => {
        const selectedAdults = parseInt(adultSelect.value);
        const maxChildren = 4 - selectedAdults;

        // Clear existing options
        childSelect.innerHTML = "";
        for (let j = 0; j <= maxChildren; j++) {
          const option = document.createElement("option");
          option.value = j;
          option.textContent = j;
          childSelect.appendChild(option);
        }

        // Update totals
        updateTotals();
      };

      // Set initial options and add event listener
      updateChildOptions();
      adultSelect.addEventListener("change", updateChildOptions);
      childSelect.addEventListener("change", updateTotals);
    }

    // Update totals whenever the number of rooms changes
    updateTotals();
  }

  // Function to calculate and display totals
  function updateTotals() {
    const dynamicDivs = document.querySelectorAll(".dynamic-div");
    const totalRooms = dynamicDivs.length;

    let totalAdults = 0;
    let totalChildren = 0;

    dynamicDivs.forEach(div => {
      const adultCount = parseInt(div.querySelector(".adultCount").value) || 0;
      const childCount = parseInt(div.querySelector(".childCount").value) || 0;

      totalAdults += adultCount;
      totalChildren += childCount;
    });

      // Determine pluralization
      const roomText = totalRooms > 1 ? "Rooms" : "Room";
      const adultText = totalAdults > 1 ? "Adults" : "Adult";
      const childText = totalChildren > 1 ? "Children" : "Child";

      // Update total display
      totalDisplay.innerHTML = `
        <span>  ${totalRooms} ${roomText} </span>
        <span>  ${totalAdults} ${adultText} </span>
        <span>  ${totalChildren} ${childText} </span>
      `;

      // Update total display
      totalDisplay_hotel.innerHTML = `
        <span>  ${totalRooms} ${roomText} </span>
        <span>  ${totalAdults} ${adultText} </span>
        <span>  ${totalChildren} ${childText} </span>
      `;

      // Update total display
      totalDisplay_readyMade.innerHTML = `
        <span>  ${totalRooms} ${roomText} </span>
        <span>  ${totalAdults} ${adultText} </span>
        <span>  ${totalChildren} ${childText} </span>
      `;

  }

  // Listen for changes in divCount
  divCount.addEventListener("change", function () {
    const count = parseInt(divCount.value) || 0;
    createDynamicDivs(count);
  });

  // Create a default single div on page load
  createDynamicDivs(1);
});

//  *********************hotel booking form*****************  
