// Arrays to hold selected values

let selectedDays = [];
let selectedDates = [];
let selectedMonths = [];

// Object to hold selected responses
const selectedResponses = {};

// Function to handle checkbox changes
function toggleCheckbox(checkbox) {
    const value = checkbox.value;
   
    alert(checkbox.value);
    const category = checkbox.name;
    const checkboxes = checkbox.parentElement.parentElement.querySelectorAll('input[type="checkbox" ]');
    alert(checkboxes)

    if (checkbox.checked) {
        checkboxes.forEach(cb => {
            if (cb !== checkbox) {
                cb.checked = false;
            }
        });
    }

    // Check the checkbox's context
    if (checkbox.closest('.calendar-days')) {
        // If the checkbox is in the calendar section
        if (checkbox.closest('.calendar-days').classList.contains('col-6')) {
            // Days section
            if (checkbox.checked) {
                selectedDays.push(value);
            } else {
                selectedDays = selectedDays.filter(day => day !== value);
            }
        } else if (checkbox.closest('.calendar-days').classList.contains('col-11')) {
            // Dates section
            if (checkbox.checked) {
                selectedDates.push(value);
            } else {
                selectedDates = selectedDates.filter(date => date !== value);
            }
        } else if (checkbox.closest('.calendar-days').classList.contains('col-10')) {
            // Months section
            if (checkbox.checked) {
                selectedMonths.push(value);
            } else {
                selectedMonths = selectedMonths.filter(month => month !== value);
            }
        }

        // Log the selected values to the console
        console.log("Selected Days:", selectedDays);
        console.log("Selected Dates:", selectedDates);
        console.log("Selected Months:", selectedMonths);
    } else {
        // If the checkbox is in the table section
        // Initialize the array for the category if it doesn't exist
        if (!selectedResponses[category]) {
            selectedResponses[category] = [];
        }

        // Add or remove the response based on checkbox state
        if (checkbox.checked) {
            selectedResponses[category].push(checkbox.parentNode.cellIndex); // Get the index of the checked checkbox
        } else {
            const index = selectedResponses[category].indexOf(checkbox.parentNode.cellIndex);
            if (index > -1) {
                selectedResponses[category].splice(index, 1); // Remove the index if unchecked
            }
        }

        // Log the selected responses to the console
        console.log("Selected Responses:", selectedResponses);
    }
    // Assuming selectedResponses is already defined and populated
    for (const category in selectedResponses) {
        if (selectedResponses.hasOwnProperty(category)) {
            const responses = selectedResponses[category]; // Get the array of responses for the category

            if (responses.length > 0) {
                const lastItem = responses[responses.length - 1]; // Get the last item in the array

                console.log("Category Name:", category); // Log the category name
                console.log("Last Selected Response Index:", lastItem); // Log the last selected response
            } else {
                console.log("Category Name:", category);
                console.log("No responses selected.");
            }
        }
    }


}
$(document).ready(function () {
    
    $('#governorate').change(function () {
        const governorateId = $(this).val();
        const citySelect = $('#city');
        citySelect.empty().append('<option value = "" > اختر </option>');

        if (governorateId) {
            $.ajax({
                url: `/Home/Getcity?Id=${governorateId}`,
                method: 'GET',
                success: function (data) {
                    data.forEach(city => {
                        citySelect.append(`<option value="${city.id}">${city.name}</option>`);
                    });
                },
                error: function (error) {
                    console.error('Error fetching cities:', error);
                }
            });
        }
    });

    
    $('#city').change(function () {
        const cityId = $(this).val();
        const areaSelect = $('#area');
        areaSelect.empty().append('<option value = "" > اختر </option>');

        if (cityId) {
            $.ajax({
                url: `/Home/Getcity?Id=${cityId}`,
                method: 'GET',
                success: function (data) {
                    data.forEach(area => {
                        areaSelect.append(`<option value="${area.id}">${area.name}</option>`);
                    });
                },
                error: function (error) {
                    console.error('Error fetching areas:', error);
                }
            });
        }
    });

    // Populate parks based on selected area
    $('#area').change(function () {
        const areaId = $(this).val();
        const parkSelect = $('#park');
        parkSelect.empty().append('<option value = "" > اختر </option>');

        if (areaId) {
            $.ajax({
                url: `/Home/Getpark?Id=${areaId}`,
                method: 'GET',
                success: function (data) {
                    data.forEach(park => {
                        parkSelect.append(`<option value="${park.id}">${park.name}</option>`);
                    });
                },
                error: function (error) {
                    console.error('Error fetching parks:', error);
                }
            });
        }
    });
});
function toggleCheckbox(checkbox) {
    const checkboxes = document.querySelectorAll('input[name="' + checkbox.name + '"]');
    if (checkbox.checked) {
        checkboxes.forEach(cb => {
            if (cb !== checkbox) {
                cb.checked = false;
            }
        });
    }
}

function validateForm() {
    const dayCheckboxes = document.querySelectorAll('input[name="Day"]');
    const dateCheckboxes = document.querySelectorAll('input[name="data"]');
    const monthCheckboxes = document.querySelectorAll('input[name="month"]');

    const isDayChecked = Array.from(dayCheckboxes).some(cb => cb.checked);
    const isDateChecked = Array.from(dateCheckboxes).some(cb => cb.checked);
    const isMonthChecked = Array.from(monthCheckboxes).some(cb => cb.checked);

    if (!isDayChecked) {
        alert("يرجى اختيار يوم واحد على الأقل.");
        return false; 
      
    }

    if (!isDateChecked) {
        alert("يرجى اختيار تاريخ واحد على الأقل.");
        return false;
    }

    if (!isMonthChecked) {
        alert("يرجى اختيار شهر واحد على الأقل.");
        return false; 
    }

    const categories = ['منطقة الخدمات', 'البويات', 'منطقة الأطفال', 'المواقف', 'المظلات', 'الطرق والممرات'];
    let allChecked = true;
   
    categories.forEach((category, index) => {
        const radios = document.getElementsByName(`response[${index}]`);
        const isChecked = Array.from(radios).some(radio => radio.checked);
        if (!isChecked) {
            allChecked = false;
            alert(`يرجى اختيار إجابة واحدة على الأقل لـ: ${category}`);
        }
    });

    return allChecked;

   
    return true;
}





