ko.observableArray.fn.sortBy = function (fields) {
    return ko.observableArray(this.sort(function (a, b) {
        $.each(fields, function (index, value) {
            var a = ko.unwrap(a[value.name]);
            var b = ko.unwrap(b[value.name]);

            if (a === b) continue;

            return a < b ? 1 : -1;
        });
    }));
};

ModPlannerViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, {}, self);

    //Set Filter
    self.sets = ko.observableArray([
        new Set("8", "Speed", "/Content/Images/speed.png"),
        new Set("3", "Critical Damage", "/Content/Images/critical_damage.png"),
        new Set("4", "Critical Chance", "/Content/Images/crit_chance.png"),
        new Set("7", "Potency", "/Content/Images/potency.png"),
        new Set("5", "Tenacity", "/Content/Images/tenacity.png"),
        new Set("1", "Health", "/Content/Images/health.png"),
        new Set("2", "Defense", "/Content/Images/defense.png"),
        new Set("6", "Offense", "/Content/Images/offense.png")
    ]);
    self.selectedSets = ko.observableArray();

    //Include Modsets filter
    self.includeModsets = ko.observable(false);

    //Primary filters
    self.squareFilters = ko.observableArray([
        new Primary("1", "Offense")
    ]);
    self.selectedSquareFilters = ko.observableArray();
    self.arrowFilters = ko.observableArray([
        new Primary("3", "Speed"),
        new Primary("1", "Offense"),
        new Primary("7", "Protection"),
        new Primary("11", "Crit Avoidance"),
        new Primary("6", "Health"),
        new Primary("10", "Accuracy"),
        new Primary("2", "Defense")
    ]);
    self.selectedArrowFilters = ko.observableArray();
    self.diamondFilters = ko.observableArray([
        new Primary("2", "Defense")
    ]);
    self.selectedDiamondFilters = ko.observableArray();
    self.triangleFilters = ko.observableArray([
        new Primary("9", "Crit Damage"),
        new Primary("5", "Crit Chance"),
        new Primary("1", "Offense"),
        new Primary("7", "Protection"),
        new Primary("6", "Health"),
        new Primary("2", "Defense")
    ]);
    self.selectedTriangleFilters = ko.observableArray();
    self.circleFilters = ko.observableArray([
        new Primary("7", "Protection"),
        new Primary("6", "Health")
    ]);
    self.selectedCircleFilters = ko.observableArray();
    self.crossFilters = ko.observableArray([
        new Primary("8", "Potency"),
        new Primary("1", "Offense"),
        new Primary("7", "Protection"),
        new Primary("6", "Health"),
        new Primary("4", "Tencaity"),
        new Primary("2", "Defense")
    ]);
    self.selectedCrossFilters = ko.observableArray();

    //Sorts
    self.sorts = ko.observableArray([
        new Sort("3", "Speed"),
        new Sort("5", "Critical Chance"),
        new Sort("9", "Critical Damage"),
        new Sort("101", "Flat Offense"),
        new Sort("201", "Percentage Offense"),
        new Sort("8", "Potency"),
        new Sort("10", "Accuracy"),
        new Sort("107", "Flat Protection"),
        new Sort("207", "Percentage Protection"),
        new Sort("106", "Flat Health"),
        new Sort("206", "Percentage Health"),
        new Sort("102", "Flat Defense"),
        new Sort("202", "Percentage Defense"),
        new Sort("4", "Tenacity"),
        new Sort("11", "Critical Avoidance")
    ]);
    self.selectedSorts = ko.observableArray([new SortRow(self.sorts[0])]);
    self.addSort = function() {
        self.selectedSorts.push(new SortRow(self.sorts[0]));
    };
    self.removeSort = function(sort) {
        self.selectedSorts.remove(sort);
    }

    //Mods
    self.filterAndSort = function (slot, primaryFilters) {
        var setFilters = self.selectedSets();

        //Slot Filter
        var results = ko.utils.arrayFilter(self.Mods(), function (mod) {
            return mod.Slot() === slot;
        });

        //Set Filter
        ko.utils.arrayForEach(setFilters, function () {
            results = ko.utils.arrayFilter(results, function (mod) {
                return setFilters.indexOf(mod.Set().toString()) > -1;
            });
        });

        //Planned Filter
        if (!self.includeModsets()) {
            results = ko.utils.arrayFilter(results, function (mod) {
                return !mod.IsInModSet();
            });
        }

        //Primary Filter
        ko.utils.arrayForEach(primaryFilters, function () {
            results = ko.utils.arrayFilter(results, function (mod) {
                return primaryFilters.indexOf(mod.PrimaryType().toString()) > -1;
            });
        });

        return results.sort(function (a, b) {
            for (var i = 0; i < self.selectedSorts().length; i++) {
                if (!self.selectedSorts()[i].sort)
                    return 1;

                var sortProp = self.selectedSorts()[i].sort.name;
                sortProp = sortProp.replace("ical ", "");
                sortProp = sortProp.replace(" ", "");

                console.log(sortProp);
                console.log(a);
                console.log(b);

                var firstItem = ko.unwrap(a[sortProp]);
                var secondItem = ko.unwrap(b[sortProp]);

                console.log(firstItem);
                console.log(secondItem);

                if (firstItem === secondItem) continue;

                return firstItem < secondItem ? 1 : -1;
            }
        });
    };
    self.squareMods = ko.computed(function () {
        var primaryFilters = self.selectedSquareFilters();
        var results = self.filterAndSort(1, primaryFilters);
        return results;
    });
    self.arrowMods = ko.computed(function () {
        var primaryFilters = self.selectedArrowFilters();
        var results = self.filterAndSort(2, primaryFilters);
        return results;
    });
    self.diamondMods = ko.computed(function () {
        var primaryFilters = self.selectedDiamondFilters();
        var results = self.filterAndSort(3, primaryFilters);
        return results;
    });
    self.triangleMods = ko.computed(function () {
        var primaryFilters = self.selectedTriangleFilters();
        var results = self.filterAndSort(4, primaryFilters);
        return results;
    });
    self.circleMods = ko.computed(function () {
        var primaryFilters = self.selectedCircleFilters();
        var results = self.filterAndSort(5, primaryFilters);
        return results;
    });
    self.crossMods = ko.computed(function () {
        var primaryFilters = self.selectedCrossFilters();
        var results = self.filterAndSort(6, primaryFilters);
        return results;
    });
}

function Set(id, name, imgUrl) {
    this.id = id;
    this.name = name;
    this.imgUrl = imgUrl;
}

function Primary(id, name) {
    this.id = id;
    this.name = name;
}

function Sort(id, name) {
    this.id = id;
    this.name = name;
}

function SortRow(sort) {
    this.sort = sort;
}