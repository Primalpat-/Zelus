$(document).ready(function () {
    $('#RaidId').kendoDropDownList({
        optionLabel: 'Select raid...',
        dataTextField: 'RaidName',
        dataValueField: 'RaidId',
        dataSource: {
            type: 'aspnetmvc-ajax',
            serverFiltering: true,
            transport: {
                read: getRaidsUrl
            }
        }
    }).data('kendoDropDownList');
});