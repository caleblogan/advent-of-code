use std::env;
use std::fs;
use std::collections::HashSet;


fn main() {
    let file_path = "input.txt";
    let contents = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");

    let lines = contents.split('\n');
    let mut res = 0;

    for row in lines {
        let mut winners = HashSet::new();
        winners.insert("boots".to_string());
        let mut card = row.split(":").collect::<Vec<_>>()[1];
        let mut parts = card.split(" | ").collect::<Vec<_>>();
        for num in parts[0].split(" ") {
            if num != "" {
                winners.insert(num.to_string());
            }
        }
        let mut my_winners = 0;
        for num in parts[1].split(" ") {
            if winners.contains(num) {
                my_winners += 1;
            }
        }
        if my_winners > 0 {
            res += 2u64.pow(my_winners-1);
        }
    }
    println!("Res:\n{res}");
}