var countrySelect = $("#countrySelect");
var stateSelect = $("#stateSelect");
var lgSelect = $("#lgSelect");
var msSelect = $("#msSelect");
var sexSelect = $("#sexSelect");
var religionSelect = $("#religionSelect");


var sampleTypeCategorySelect = $(".sampleTypeCategorySelect");
var featureSelect = $(".featureSelect");
var parameterSelect = $(".parameterSelect");
var qualityControlSelect = $(".qualityControlSelect");
var categorySelect = $(".categorySelect");



msSelect.select2({
    placeholder: "Are they married?",
    width: "100%",
    allowClear: true
});

sexSelect.select2({
    placeholder: "What is their gender?",
    width: "100%",
    allowClear: true
});

religionSelect.select2({
    placeholder: "What is your religion?",
    width: "100%",
    allowClear: true
});

function initializeSelectWithPlaceholder(select, placeHolder)
{
    select.select2({

        theme: 'bootstrap4'
    });
}

function resetSelect(select)
{
    select.select2("destroy");
    select.empty();
    select.prepend("<option></option>");
}

function mapResults(data)
{
    var results = [];
    for (var i = 0; i < data.length; i++)
    {
        var result = data[i];
        results.push({
            id: result.id,
            text: result.name
        });
    }
    return {
        results
    };
}

function mapNameToName(data)
{
    var results = [];
    for (var i = 0; i < data.length; i++)
    {
        var result = data[i];
        results.push({
            id: result.name,
            text: result.name
        });
    }
    return {
        results
    };
}

function mapAllProgramsResults(data)
{
    var results = [];
    for (var i = 0; i < data.length; i++)
    {
        var result = data[i];

        var collegeIndex = selectArrayIndexOf(results, result.collegeCode);
        if (collegeIndex === -1)
        {
            results.push({
                text: result.collegeCode,
                children: [
                    {
                        text: result.departmentCode,
                        children: [
                            {
                                id: result.id,
                                text: result.name
                            }
                        ]
                    }
                ]
            });
            continue;
        }
        var departmentIndex = selectArrayIndexOf(results[collegeIndex].children, result.departmentCode);
        if (departmentIndex === -1)
        {
            results[collegeIndex].children.push({
                text: result.departmentCode,
                children: [
                    {
                        id: result.id,
                        text: result.name
                    }
                ]
            });
            continue;
        }

        results[collegeIndex].children[departmentIndex].children.push({
            id: result.id,
            text: result.name
        });
    }


    return {
        results
    };
}

function selectArrayIndexOf(array, text)
{
    for (var i in array)
    {
        if (array.hasOwnProperty(i) && array[i].text === text)
        {
            return i;
        }
    }

    return -1;
}

function setupLocalGovernmentSelects(lgUrl, statesUrl, nationsUrl) {
    function setupStateSelectListeners() {
        stateSelect.on("select2:select",
            function (e2) {
                var selected = e2.params.data;
                resetSelect(lgSelect);
                lgSelect.select2({
                    placeholder: "Choose a local government",
                    allowClear: true,
                    width: "100%",
                    ajax: {
                        dataType: "json",
                        url: lgUrl + "?stateId=" + selected.id,
                        processResults: function (data) {
                            return mapResults(data);
                        }
                    }
                });
            });

        stateSelect.on("select2:deselect",
            function (e2) {
                resetSelect(lgSelect);
                initializeSelectWithPlaceholder(lgSelect, "Choose a local government");
            });
    }

    countrySelect.on("select2:select",
        function (e) {
            var selected = e.params.data;
            resetSelect(stateSelect);
            stateSelect.select2({
                placeholder: "Choose a state",
                allowClear: true,
                width: "100%",
                ajax: {
                    dataType: "json",
                    url: statesUrl + "?nationId=" + selected.id,
                    processResults: function (data) {
                        return mapResults(data);
                    }
                }
            });
            setupStateSelectListeners();
        });

    countrySelect.on("select2:deselect",
        function (e) {
            resetSelect(stateSelect);
            initializeSelectWithPlaceholder(stateSelect, "Choose a state");
        });


    countrySelect.select2({
        placeholder: "Choose a country",
        allowClear: true,
        width: "100%",
        ajax: {
            dataType: "json",
            url: nationsUrl,
            processResults: function (data) {
                return mapResults(data);
            }
        }
    });

    initializeSelectWithPlaceholder(stateSelect, "Choose a state");
    initializeSelectWithPlaceholder(lgSelect, "Choose a local government");
}




function setupsampleTypeCategorySelect(url, placeholder = "Select sample type Category") {
    setupBasicSelect(sampleTypeCategorySelect, url, placeholder);
}

function setupFeatureSelect(url, placeholder = "Select feature") {
    setupBasicSelect(featureSelect, url, placeholder);
}

function setupCategorySelect(url, placeholder = "Select category") {
    setupBasicSelect(categorySelect, url, placeholder);
}
function setupParameterSelect(url, placeholder = "Select parameter") {
    setupBasicSelect(parameterSelect, url, placeholder);
}
function setupQualityControlSelect(url, placeholder = "Select quality control") {
    setupBasicSelect(qualityControlSelect, url, placeholder);
}




function setupBasicSelect(select, url, placeholder)
{
    select.select2({
        theme: 'bootstrap4',
        ajax: {
            dataType: "json",
            url: url,
            processResults: function(data)
            {
                return mapResults(data);
            }
        }
    });

   
}
