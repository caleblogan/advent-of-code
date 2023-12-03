package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func isDigit(c byte) bool {
	if c >= '0' && c <= '9' {
		return true
	}
	return false
}

func isSymbol(c byte) bool {
	if isDigit(c) || c == '.' {
		return false
	}
	return true
}

func symbolAt(lines []string, r int, c int) bool {
	if r < 0 || r >= len(lines) || c < 0 || c >= len(lines[0]) || !isSymbol(lines[r][c]) {
		return false
	}
	return true
}

func bySymbol(lines []string, r int, c int) bool {
	return symbolAt(lines, r-1, c-1) ||
		symbolAt(lines, r-1, c) ||
		symbolAt(lines, r-1, c+1) ||
		symbolAt(lines, r, c+1) ||
		symbolAt(lines, r+1, c+1) ||
		symbolAt(lines, r+1, c) ||
		symbolAt(lines, r+1, c-1) ||
		symbolAt(lines, r, c-1)
}

func main() {
	dat, err := os.ReadFile("input.txt")
	check(err)
	lines := strings.Split(string(dat), "\n")
	res := 0
	for r := 0; r < len(lines); r++ {
		buf := ""
		foundSymbol := false
		for c := 0; c < len(lines[r]); c++ {
			if isDigit(lines[r][c]) {
				buf += string(lines[r][c])
				if bySymbol(lines, r, c) {
					foundSymbol = true
				}
			} else {
				if foundSymbol && len(buf) > 0 {
					fmt.Printf("found %s r=(%d, %d)\n", buf, r, c)
					conv, _ := strconv.Atoi(buf)
					res += conv
				}
				foundSymbol = false
				buf = ""
			}
		}
		if foundSymbol && len(buf) > 0 {
			fmt.Printf("found %s r=(%d) at end of line\n", buf, r)
			conv, _ := strconv.Atoi(buf)
			res += conv
		}
	}
	fmt.Printf("RES: %d\n", res)
}
