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

func isNumAt(lines []string, r int, c int) bool {
	if r < 0 || r >= len(lines) || c < 0 || c >= len(lines[0]) || !isDigit(lines[r][c]) {
		return false
	}
	return true
}

func getNumAt(lines []string, r int, c int) int {
	l := c
	for isNumAt(lines, r, l-1) {
		l -= 1
	}
	buff := ""
	for isNumAt(lines, r, l) {
		buff += string(lines[r][l])
		l += 1
	}
	res, _ := strconv.Atoi(buff)
	return res
}

func numsAround(lines []string, r int, c int) []int {
	var res []int
	// Top row
	if isNumAt(lines, r-1, c) {
		res = append(res, getNumAt(lines, r-1, c))
	} else {
		if isNumAt(lines, r-1, c-1) {
			res = append(res, getNumAt(lines, r-1, c-1))
		}
		if isNumAt(lines, r-1, c+1) {
			res = append(res, getNumAt(lines, r-1, c+1))
		}
	}

	// Right
	if isNumAt(lines, r, c+1) {
		res = append(res, getNumAt(lines, r, c+1))
	}

	// Bottom row
	if isNumAt(lines, r+1, c) {
		res = append(res, getNumAt(lines, r+1, c))
	} else {
		if isNumAt(lines, r+1, c+1) {
			res = append(res, getNumAt(lines, r+1, c+1))
		}
		if isNumAt(lines, r+1, c-1) {
			res = append(res, getNumAt(lines, r+1, c-1))
		}
	}

	// Left
	if isNumAt(lines, r, c-1) {
		res = append(res, getNumAt(lines, r, c-1))
	}

	return res
}

func main() {
	dat, err := os.ReadFile("input.txt")
	check(err)
	lines := strings.Split(string(dat), "\n")
	res := 0
	for r := 0; r < len(lines); r++ {
		for c := 0; c < len(lines[r]); c++ {
			if lines[r][c] == '*' {
				nums := numsAround(lines, r, c)
				if len(nums) == 2 {
					res += nums[0] * nums[1]
				}
			}
		}
	}
	fmt.Printf("RES: %d\n", res)
}
