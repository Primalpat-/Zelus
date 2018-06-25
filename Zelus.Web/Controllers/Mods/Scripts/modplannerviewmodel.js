function Set(id, name, imgUrl) {
    this.id = id;
    this.name = name;
    this.imgUrl = imgUrl;
}

function Primary(id, name) {
    this.id = id;
    this.name = name;
}

function Sort(priority, initialStat) {
    var self = this;
    self.priority = priority;
    self.stat = ko.observable(initialStat);
}

ModPlannerViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    //Set Filter
    self.availableSets = [
        new Set("8", "Speed", "/Content/Images/speed.png"),
        new Set("3", "Critical Damage", "/Content/Images/critical_damage.png"),
        new Set("4", "Critical Chance", "/Content/Images/crit_chance.png"),
        new Set("7", "Potency", "/Content/Images/potency.png"),
        new Set("5", "Tenacity", "/Content/Images/tenacity.png"),
        new Set("1", "Health", "/Content/Images/health.png"),
        new Set("2", "Defense", "/Content/Images/defense.png"),
        new Set("6", "Offense", "/Content/Images/offense.png")
    ];
    self.selectedSets = ko.observableArray();

    //Include Modsets filter
    self.includeModsets = ko.observable(false);

    //Character filter
    self.selectedCharacter = ko.observable(0);

    //Primary filters
    self.availableSquareFilters = [
        new Primary("1", "Offense")
    ];
    self.selectedSquareFilters = ko.observableArray();
    self.availableArrowFilters = [
        new Primary("3", "Speed"),
        new Primary("1", "Offense"),
        new Primary("7", "Protection"),
        new Primary("11", "Crit Avoidance"),
        new Primary("6", "Health"),
        new Primary("10", "Accuracy"),
        new Primary("2", "Defense")
    ];
    self.selectedArrowFilters = ko.observableArray();
    self.availableDiamondFilters = [
        new Primary("2", "Defense")
    ];
    self.selectedDiamondFilters = ko.observableArray();
    self.availableTriangleFilters = [
        new Primary("9", "Crit Damage"),
        new Primary("5", "Crit Chance"),
        new Primary("1", "Offense"),
        new Primary("7", "Protection"),
        new Primary("6", "Health"),
        new Primary("2", "Defense")
    ];
    self.selectedTriangleFilters = ko.observableArray();
    self.availableCircleFilters = [
        new Primary("7", "Protection"),
        new Primary("6", "Health")
    ];
    self.selectedCircleFilters = ko.observableArray();
    self.availableCrossFilters = [
        new Primary("8", "Potency"),
        new Primary("1", "Offense"),
        new Primary("7", "Protection"),
        new Primary("6", "Health"),
        new Primary("4", "Tenacity"),
        new Primary("2", "Defense")
    ];
    self.selectedCrossFilters = ko.observableArray();

    //Sorts
    self.availableSorts = [
        { name: "Speed" },
        { name: "Critical Chance" },
        { name: "Critical Damage" },
        { name: "Flat Offense" },
        { name: "Percentage Offense" },
        { name: "Potency" },
        { name: "Accuracy" },
        { name: "Flat Protection" },
        { name: "Percentage Protection" },
        { name: "Flat Health" },
        { name: "Percentage Health" },
        { name: "Flat Defense" },
        { name: "Percentage Defense" },
        { name: "Tenacity" },
        { name: "Critical Avoidance" }
    ];
    self.selectedSorts = ko.observableArray([new Sort(0, self.availableSorts[0])]);
    self.addSort = function () {
        var priority = self.selectedSorts()[self.selectedSorts().length - 1].priority + 1;
        self.selectedSorts.push(new Sort(priority, self.availableSorts[0]));
    };
    self.removeSort = function(sort) {
        self.selectedSorts.remove(sort);
    }

    //Mods
    self.filterAndSort = function (slot, primaryFilters, sorts) {
        var setFilters = self.selectedSets();
        var charFilter = self.selectedCharacter();

        //Slot Filter
        var results = ko.utils.arrayFilter(self.Mods(), function (mod) {
            return mod.Slot() === slot;
        });

        //Character Filter
        console.log(charFilter);
        if (charFilter !== 0) {
            console.log("filtering mods based on character...");
            results = ko.utils.arrayFilter(results, function (mod) {
                console.log("Does " + mod.CharacterId + " equal " + charFilter + "?");
                return mod.CharacterId() === charFilter ||
                       mod.IsChecked();
            });
        }

        //Set Filter
        ko.utils.arrayForEach(setFilters, function () {
            results = ko.utils.arrayFilter(results, function (mod) {
                return setFilters.indexOf(mod.Set().toString()) > -1 ||
                       mod.IsChecked() === true;
            });
        });

        //Planned Filter
        if (!self.includeModsets()) {
            results = ko.utils.arrayFilter(results, function (mod) {
                return !mod.IsInModSet() ||
                        mod.IsChecked() === true;
            });
        }

        //Primary Filter
        ko.utils.arrayForEach(primaryFilters, function () {
            results = ko.utils.arrayFilter(results, function (mod) {
                return primaryFilters.indexOf(mod.PrimaryType().toString()) > -1 ||
                       mod.IsChecked() === true;
            });
        });

        return results.sort(function (a, b) {
            for (var i = 0; i < sorts.length; i++) {
                if (!sorts[i] || b.IsChecked() === true)
                    return 1;

                if (a.IsChecked() === true)
                    return -1;
                
                var sortProp = sorts[i].stat().name;
                sortProp = sortProp.replace("ical ", "");
                sortProp = sortProp.replace(" ", "");

                var firstItem = ko.unwrap(a[sortProp]);
                var secondItem = ko.unwrap(b[sortProp]);

                if (firstItem === secondItem) continue;

                return firstItem < secondItem ? 1 : -1;
            }
        });
    };
    self.squareMods = ko.computed(function () {
        var sorts = self.selectedSorts();
        var primaryFilters = self.selectedSquareFilters();
        var results = self.filterAndSort(1, primaryFilters, sorts);
        return results;
    });
    self.arrowMods = ko.computed(function () {
        var sorts = self.selectedSorts();
        var primaryFilters = self.selectedArrowFilters();
        var results = self.filterAndSort(2, primaryFilters, sorts);
        return results;
    });
    self.diamondMods = ko.computed(function () {
        var sorts = self.selectedSorts();
        var primaryFilters = self.selectedDiamondFilters();
        var results = self.filterAndSort(3, primaryFilters, sorts);
        return results;
    });
    self.triangleMods = ko.computed(function () {
        var sorts = self.selectedSorts();
        var primaryFilters = self.selectedTriangleFilters();
        var results = self.filterAndSort(4, primaryFilters, sorts);
        return results;
    });
    self.circleMods = ko.computed(function () {
        var sorts = self.selectedSorts();
        var primaryFilters = self.selectedCircleFilters();
        var results = self.filterAndSort(5, primaryFilters, sorts);
        return results;
    });
    self.crossMods = ko.computed(function () {
        var sorts = self.selectedSorts();
        var primaryFilters = self.selectedCrossFilters();
        var results = self.filterAndSort(6, primaryFilters, sorts);
        return results;
    });
    self.saveModset = function() {
        var showConfirm = false;
        var modIds = [];

        var selectedMods = ko.utils.arrayFilter(self.Mods(), function (mod) {
            return mod.IsChecked();
        });

        for (var i = 0; i < selectedMods.length; i++) {
            var mod = selectedMods[i];
            if (mod.IsInModSet() === true)
                showConfirm = true;
            modIds.push(mod.Id);
        }

        var confirmation = false;
        if (showConfirm)
            confirmation = confirm("Saving a mod that has already been planned will delete the modset it is a part of.  Continue?");
         else
            confirmation = true;

        if (confirmation === true) {
            $.post(saveModsetUrl, { "playerId": self.PlayerId(), "modIds": modIds }, function (result) {
                if (!result.Success)
                    toastr.error(result.Messages[0], "Error saving modset:");
                else {
                    toastr.success("Modset successfully saved.");
                    for (var i = 0; i < selectedMods.length; i++) {
                        var mod = selectedMods[i];
                        mod.IsInModSet(true);
                        mod.IsChecked(false);
                    }
                }
            });
        }
    }
}