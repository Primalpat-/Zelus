StrategyViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    self.availableStrategies = [
        { strategyName: "Defensive", value: 1 },
        { strategyName: "Balanced", value: 2 },
        { strategyName: "Aggressive", value: 3 }
    ];

    self.calculateStrategy = function () {
        
        $("#strategyFormSubmit").find("svg").addClass("fa-spin");

        $.ajax(calculateStrategyUrl,
            {
                data: ko.toJSON({ strategyType: self.StrategyType, playerName: self.PlayerName }),
                type: "post",
                contentType: "application/json",
                fail: function () {
                    toastr.error("Could not connect to the server...", "Error while strategizing:");
                    $("#strategyFormSubmit").find("svg").removeClass("fa-spin");
                },
                success: function (result) {
                    if (!result.Success)
                        toastr.error(result.Messages[0], "Error while strategizing:");
                    else
                        ko.mapping.fromJS(result.Value, {}, self.Strategy);

                    $("#strategyFormSubmit").find("svg").removeClass("fa-spin");
                }
            });
    };
}