def firstDigit(s)
    for i in 0..s.length-1
        if s[i] >= '1' && s[i] <= '9'
            return s[i]
        elsif s[i..i+2] == "one" or s[i..i+2] == "eno"
            return '1'
        elsif s[i..i+2] == "two" or s[i..i+2] == "owt"
            return '2'
        elsif s[i..i+4] == "three" or s[i..i+4] == "eerht"
            return '3'
        elsif s[i..i+3] == "four" or s[i..i+3] == "ruof"
            return '4'
        elsif s[i..i+3] == "five" or s[i..i+3] == "evif"
            return '5'
        elsif s[i..i+2] == "six" or s[i..i+2] == "xis"
            return '6'
        elsif s[i..i+4] == "seven" or s[i..i+4] == "neves"
            return '7'
        elsif s[i..i+4] == "eight" or s[i..i+4] == "thgie"
            return '8'
        elsif s[i..i+3] == "nine" or s[i..i+3] == "enin"
            return '9'
        end
    end
    return ""
end

if __FILE__ == $0
    # 1abc2
    # fiveabc6
    file = File.open("input.txt", "r")
    lines = file.readlines
    sum = 0
    lines.each { |l|
        digits = ""
        puts l
        digits += firstDigit(l)
        digits += firstDigit(l.reverse)
        sum += digits.to_i
    }
    puts "the sum is #{sum}"
end
