import FileProvider.Manager
let manager = FileManager.default

var sumOfPowerSets = 0
let filename = "\(manager.homeDirectoryForCurrentUser.relativePath)/dev/advent-of-code/2023/2/input.txt"
do {
    let contents = try String(contentsOfFile: filename)
    let lines = contents.split(separator:"\n")
    for game in lines {
        let subsetsStr = game.split(separator:":")[1].trimmingCharacters(in: .whitespacesAndNewlines)
        let subsets = subsetsStr.split(separator:";")
        var maxRed = 0
        var maxBlue = 0
        var maxGreen = 0
        for subset in subsets {
            let blockStrs = subset.split(separator:",")
            for block in blockStrs {
                let countColor = block.split(separator:" ")
                let count = Int(countColor[0]) ?? 0
                let color = countColor[1]
                switch (color) {
                case "red":
                    maxRed = max(maxRed, count)
                    break;
                case "blue":
                    maxBlue = max(maxBlue, count)
                    break;
                case "green":
                    maxGreen = max(maxGreen, count)
                    break;
                default:
                    print("invalid commmand")
                }
            }
        }
        // print("Min needed = \(maxRed) \(maxGreen) \(maxBlue)")
        sumOfPowerSets += (maxRed * maxGreen * maxBlue)
    }
}
catch {}
print("Sum of power sets \(sumOfPowerSets)")
