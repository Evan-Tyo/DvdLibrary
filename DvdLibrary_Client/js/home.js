/*
 * JS file for DvdLibrary_Client
 * With home.html & home.css
*/
var url = 'https://dvdlibraryservice.azurewebsites.net/';
//var url = 'https://localhost:44350/';

// Document ready function
$(document).ready(function () {
    loadDvdTable();
    createDvdButton();
    searchButton();
    saveEditButton();
});

/*
 * General Use
*/

// Clear error messages for all pages
function clearErrorMessages() {
    $('#errorMessagesInitial').empty();
    $('#errorMessagesEdit').empty();
    $('#errorMessagesCreate').empty();
}

// Display Error messages with fadeout
function errorMessage(page, message) {
    clearErrorMessages();

    $('#errorMessages' + page)
        .append($('<li id="errorFade">')
            .attr({ class: 'list-group-item list-group-item-danger' })
            .text(message));

    setTimeout(function () {
        $('#errorFade').fadeOut('fast');
    }, 3000);
}

/*
 * Initial Page
*/

// Load the dvd table data from the database
function loadDvdTable() {
    clearTableRows();
    var tableContent = $('#tableContent');

    // Perform ajax call GET
    $.ajax({
        type: 'GET',
        url: url + 'dvds',
        success: function (dvdArray) {

            // Loop through database to assign data and append into table
            $.each(dvdArray, function (index, dvd) {
                var id = dvd.id;
                var title = dvd.title;
                var releaseYear = dvd.releaseYear;
                var director = dvd.director;
                var rating = dvd.rating;
                var notes = dvd.notes;

                tableContent.append(loadRows(id, title, releaseYear, director, rating));
            })
        },

        error: function () {
            errorMessage('Initial', 'Error calling web service. Please try again later.');
        }

    });
}

// Row data for appending into initial page tables
function loadRows(id, title, releaseYear, director, rating) {

    var row = '<tr>';
    row += '<td>';
    row += '<button type="button" class="btn btn-link" onclick="displayDvd(' + id + ')">' + title + '</button>';
    row += '</td>';
    row += '<td>' + releaseYear + '</td>';
    row += '<td>' + director + '</td>';
    row += '<td>' + rating + '</td>';
    row += '<td>';
    row += '<button type="button" class="btn btn-link" onclick="editDvd(' + id + ')">Edit</button>';
    row += '<label for="separateButtons">|</label >';
    row += '<button type="button" class="btn btn-link" onclick="deleteDvd(' + id + ')">Delete</button>';
    row += '</td>';
    row += '</tr>';

    return row;
}

// Clear table rows
function clearTableRows() {
    clearErrorMessages();
    $('#tableContent').empty();
}

// Load initial page, reload table
function loadInitialPage() {
    loadDvdTable();
    resetSearchCategoryTerm();
    $('#initialPage').show();
}

// Create Dvd button on initial banner
function createDvdInitial() {
    $('#createButtonInitial').on('click', function () {
        $('#initialPage').hide();
        $('#createDvd').show();
    });
}

// Reset button for search functionality
function resetInitial() {
    $('#initialPage').hide();
    loadInitialPage();
}

// Reset search category and term
function resetSearchCategoryTerm() {
    $('#searchBar').val('')
    $('#searchCategoryInput').val('searchCategory');
}

// Edit Dvd button per dvd
function editDvd(id) {
    $('#initialPage').hide();
    $('#editDvd').show();

    var editDvdBanner = $('#editDvdBanner');

    // Perform ajax call GET
    $.ajax({
        type: 'GET',
        url: url + 'dvd/' + id,

        // Assign data from database
        success: function (data, status) {
            var title = data.title;
            var releaseYear = data.releaseYear;
            var director = data.director;
            var rating = data.rating;
            var notes = data.notes;

            // Display data in edit inputs 
            $("#editTitleInput").val(title);
            $("#editYearInput").val(releaseYear);
            $("#editDirectorInput").val(director);
            $('#editRatingInput').val(rating);
            $("#editNotesInput").val(notes);

            // Append Edit dvd banner for title
            var row = '<h2 id="editDvdBannerH1">';
            row += '<input type="hidden" id="dvdIdForSave" value="' + id + '" required>';
            row += title;
            row += '</h2>';

            editDvdBanner.append(row);
        },

        error: function () {
            errorMessage('Edit', 'Error calling web services. Please try again later.');
        }
    })
}

// Delete Dvd button per dvd
function deleteDvd(id) {
    var confirmDelete = confirm('Are you sure you want to delete this Dvd from your collection?');

    // Display confirm message and choice
    if (confirmDelete == true) {

        // Perform ajax call DELETE
        $.ajax({
            type: 'DELETE',
            url: url + 'dvd/' + id,

            // Reload initial page upon success
            success: function () {
                loadInitialPage();
            }
        });
    }
}

// Search button functionality
function searchButton() {

    // On click event for Search Button
    $('#searchButton').click(function (event) {
        clearErrorMessages();
        var searchTerm = $('#searchBar').val();
        var searchCategory = $('#searchCategoryInput').find(":selected").text();

        // Check if the Search Category & Search Term are set
        if ((searchBar.length === 0) || (searchCategory == 'Search Category')) {
            resetSearchCategoryTerm();
            errorMessage('Initial', 'Search Category and Term are both required');
        }

        // Perform Search action based on Search Category set
        else {
            if (searchCategory == 'Title') {
                searchByTitle(searchTerm);
            }

            if (searchCategory == 'Release Year') {
                // Parse term as integer and check if year is within range
                searchTerm = parseInt(searchTerm)

                // Set first year to 1000 for valid data entry
                if (searchTerm >= 1000 && searchTerm <= 9999) {
                    searchByReleaseYear(searchTerm);
                }
                else {
                    errorMessage('Initial', 'Release year must be a valid 4-digit year.')
                }
            }

            else if (searchCategory == 'Director') {
                searchByDirector(searchTerm);
            }

            else if (searchCategory == 'Rating') {
                searchByRating(searchTerm);
            }
        }
    });
}

// Search the dvd database by title
function searchByTitle(titleSearch) {

    // Perform ajax call GET
    $.ajax({
        type: 'GET',
        url: url + 'dvds/title/' + titleSearch,
        success: function (dvdArray) {

            // Check if the database returned any data
            if (dvdArray.length === 0) {
                resetSearchCategoryTerm();
                errorMessage('Initial', 'Requested title not in the database.');
            }

            // Reset page and collect data from database
            else {
                //resetSearchCategoryTerm();
                clearTableRows();
                var tableContent = $('#tableContent');

                // Loop through database to assign data and append into table
                $.each(dvdArray, function (index, dvd) {
                    var id = dvd.id;
                    var title = dvd.title;
                    var releaseYear = dvd.releaseYear;
                    var director = dvd.director;
                    var rating = dvd.rating;

                    tableContent.append(loadRows(id, title, releaseYear, director, rating));

                })
            }
        },

        error: function () {
            errorMessage('Initial', 'Error calling web service. Please try again later.');
        }
    });
}

// Search the dvd database by release year
function searchByReleaseYear(releaseYearSearch) {
    var count = 0;

    // Perform ajax call GET
    $.ajax({
        type: 'GET',
        url: url + 'dvds/year/' + releaseYearSearch,
        success: function (dvdArray) {

            // Check if the database returned any data
            if (dvdArray.length === 0) {
                resetSearchCategoryTerm();
                errorMessage('Initial', 'Requested release year not in the database.');
            }

            // Reset page and collect data from database
            else {
                resetSearchCategoryTerm();
                clearTableRows();
                var tableContent = $('#tableContent');

                // Loop through database to assign data and append into table
                $.each(dvdArray, function (index, dvd) {
                    var id = dvd.id;
                    var title = dvd.title;
                    var releaseYear = dvd.releaseYear;
                    var director = dvd.director;
                    var rating = dvd.rating;

                    // Increment count if search term is an exact match
                    if (releaseYearSearch === releaseYear) {
                        count += 1;
                    }

                    tableContent.append(loadRows(id, title, releaseYear, director, rating));
                })

                // If search term is not an exact match, reset page and display error message
                if (count == 0) {
                    resetInitial();
                    errorMessage('Initial', 'Requested release year not in the database. 2');
                }
            }
        },

        error: function () {
            errorMessage('Initial', 'Error calling web service. Please try again later.');
        }
    });
}

// Search the dvd database by director
function searchByDirector(directorSearch) {

    // Perform ajax call GET
    $.ajax({
        type: 'GET',
        url: url + 'dvds/director/' + directorSearch,
        success: function (dvdArray) {

            // Check if the database returned any data
            if (dvdArray.length === 0) {
                resetSearchCategoryTerm();
                errorMessage('Initial', 'Requested director not in the database.');
            }

            // Reset page and collect data from database
            else {
                resetSearchCategoryTerm();
                clearTableRows();
                var tableContent = $('#tableContent');

                // Loop through database to assign data and append into table
                $.each(dvdArray, function (index, dvd) {
                    var id = dvd.id;
                    var title = dvd.title;
                    var releaseYear = dvd.releaseYear;
                    var director = dvd.director;
                    var rating = dvd.rating;

                    tableContent.append(loadRows(id, title, releaseYear, director, rating));
                })
            }
        },

        error: function () {
            errorMessage('Initial', 'Error calling web service. Please try again later.');
        }
    });
}

// Search the dvd database by rating
function searchByRating(ratingSearch) {
    var count = 0;

    // Perform ajax call GET
    $.ajax({
        type: 'GET',
        url: url + 'dvds/rating/' + ratingSearch,
        success: function (dvdArray) {

            // Check if the database returned any data
            if (dvdArray.length === 0) {
                resetSearchCategoryTerm();
                errorMessage('Initial', 'Requested rating not in the database.');
            }

            // Reset page and collect data from database
            else {
                resetSearchCategoryTerm();
                clearTableRows();
                var tableContent = $('#tableContent');

                // Loop through database to assign data and append into table
                $.each(dvdArray, function (index, dvd) {
                    var id = dvd.id;
                    var title = dvd.title;
                    var releaseYear = dvd.releaseYear;
                    var director = dvd.director;
                    var rating = dvd.rating;

                    // Increment count if search term is an exact match
                    if (ratingSearch === rating) {
                        count += 1;
                    }

                    tableContent.append(loadRows(id, title, releaseYear, director, rating));
                })

                // If search term is not an exact match, reset page and display error message
                if (count == 0) {
                    resetInitial();
                    errorMessage('Initial', 'Requested rating not in the database.');
                }
            }
        },

        error: function () {
            errorMessage('Initial', 'Error calling web service. Please try again later.');
        }
    });
}

/*
 * Display Page
*/

// Display Dvd information
function displayDvd(contactId) {
    clearErrorMessages();

    // Set varialbes to append data and rows
    var displayTitle = $('#displayTitle');
    var releaseYearDisplayLabel = $('#releaseYearDisplayLabel');
    var directorDisplayLabel = $('#directorDisplayLabel');
    var ratingDisplayLabel = $('#ratingDisplayLabel');
    var notesDisplayLabel = $('#notesDisplayLabel');

    // Perform ajax call GET
    $.ajax({
        type: 'GET',
        url: url + 'dvd/' + contactId,
        success: function (data, status) {
            var title = data.title;
            var releaseYear = data.releaseYear;
            var director = data.director;
            var rating = data.rating;
            var notes = data.notes;

            // Append Title Header
            var row = '<h2 id="displayTitleHeader">';
            row += title;
            row += '</h2>';

            displayTitle.append(row);

            // Append Release Year label
            var row = '<label id="releaseYearLabel" class="dynamicDisplayLabels">';
            row += releaseYear;
            row += '</label>';

            releaseYearDisplayLabel.append(row);

            // Append Director label
            var row = '<label id="directorLabel" class="dynamicDisplayLabels">';
            row += director;
            row += '</label>';

            directorDisplayLabel.append(row);

            // Append Rating label
            var row = '<label id="ratingLabel" class="dynamicDisplayLabels">';
            row += rating;
            row += '</label>';

            ratingDisplayLabel.append(row);

            // Append Notes label
            var row = '<label id="notesLabel" class="dynamicDisplayLabels">';
            row += notes;
            row += '</label>';

            notesDisplayLabel.append(row);
        },

        error: function () {
            errorMessage('Initial', 'Error calling web service. Please try again later.');
        }
    })

    // Hide initial page and load Display page
    $('#initialPage').hide();
    $('#displayDvd').show();
}

// Back button for Display page
function backButtonDisplay() {
    $('#displayDvd').hide();
    loadInitialPage();

    clearDvdDisplay();
}

// Reset Display page fields
function clearDvdDisplay() {
    $('#displayTitleHeader').remove();
    $('#releaseYearLabel').remove();
    $('#directorLabel').remove();
    $('#ratingLabel').remove();
    $('#notesLabel').remove();
}

/*
 * Edit Page
*/

// Cancel button on edit dvd
function editDvdCancel() {
    clearErrorMessages();

    // Reset Edit page fields
    $('#editDvdBannerH1').remove();
    $('#editTitleInput').val('');
    $('#editYearInput').val('');
    $('#editDirectorInput').val('');
    $('#editRatingInput').val('rating');
    $('#editNotesInput').attr('style', '').val('');

    // Hide Edit page and load Initial page
    $('#editDvd').hide();
    loadInitialPage();
}

// Edit Dvd save button
function saveEditButton() {
    $('#saveEditButton').click(function (event) {
        var id = $('#dvdIdForSave').val();
        var checkRating = $('#editRatingInput').find(":selected").text();

        // Check if Title, Year, and Rating information have been entered
        if ($('#editTitleInput').val().length === 0) {
            errorMessage('Edit', 'Please enter the title information.');
        }
        else if (!($.isNumeric($('#editYearInput').val()))) {
            errorMessage('Edit', 'Please enter a 4-digit year.');
        }

        else if (checkRating == 'Choose Rating') {
            errorMessage('Edit', 'Please select a valid rating.');
        }

        else {

            // Perform ajax call PUT
            $.ajax({
                type: 'PUT',
                url: url + 'dvd/' + id,
                data: JSON.stringify({
                    title: $('#editTitleInput').val(),
                    releaseYear: $('#editYearInput').val(),
                    director: $('#editDirectorInput').val(),
                    rating: $('#editRatingInput').val(),
                    notes: $('#editNotesInput').val()
                }),
                headers: {
                    'Accept': 'application/json; charset=UTF-8',
                    'Content-Type': 'application/json; charset=UTF-8'
                },
                'dataType': 'text',

                // On success reload Initial Page
                success: function () {

                    editDvdCancel();
                },

                error: function () {
                    errorMessage('Edit', 'Error calling web services. Please try again later.');
                }
            })
        }
    });
}

/*
 * Create Page
*/

// Cancel button on create dvd
function createDvdCancel() {
    clearErrorMessages();

    // Reset Create page fields
    $('#createTitleInput').val('');
    $('#createYearInput').val('');
    $('#createDirectorInput').val('');
    $('#createRatingInput').val('rating');
    $('#createNotesInput').attr('style', '').val('');

    // Hide Create page and load Initial page
    $('#createDvd').hide();
    loadInitialPage();
}

// Create Dvd button on create dvd
function createDvdButton() {
    $('#createDvdButton').click(function (event) {
        var checkRating = $('#createRatingInput').find(":selected").text();

        // Check if Title, Year, and Rating information have been entered
        if ($('#createTitleInput').val().length === 0) {
            errorMessage('Create', 'Please enter the title information.');
        }

        else if (!($.isNumeric($('#createYearInput').val()))) {
            errorMessage('Create', 'Please enter a 4-digit year.');
        }

        else if (checkRating == 'Choose Rating') {
            errorMessage('Create', 'Please select a valid rating.');
        }

        else {

            // Perform ajax call POST
            $.ajax({
                type: 'POST',
                url: url + 'dvd',
                data: JSON.stringify({
                    title: $('#createTitleInput').val(),
                    releaseYear: $('#createYearInput').val(),
                    director: $('#createDirectorInput').val(),
                    rating: $('#createRatingInput').val(),
                    notes: $('#createNotesInput').val()
                }),
                headers: {
                    'Accept': 'application/json; charset=UTF-8',
                    'Content-Type': 'application/json; charset=UTF-8'
                },
                'dataType': 'json',

                // On success reload Initial Page
                success: function () {

                    createDvdCancel();

                },

                error: function () {
                    errorMessage('Create', 'Error calling web service. Please try again later.');
                }
            })
        }
    });
}

