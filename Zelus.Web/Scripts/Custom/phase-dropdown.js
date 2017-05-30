$(document).ready(function() {
    $('#PhaseId').kendoDropDownList({
        autoBind: false,
        cascadeFrom: 'RaidId',
        optionLabel: 'Select phase...',
        dataTextField: 'PhaseName',
        dataValueField: 'PhaseId',
        dataSource: {
            type: 'aspnetmvc-ajax',
            serverFiltering: true,
            transport: {
                read: getPhasesUrl
            }
        }
    }).data('kendoDropDownList');
});