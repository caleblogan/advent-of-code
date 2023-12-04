use std::fs;
use std::collections::HashSet;

struct Card {
    number: u32,
    winners: HashSet<u32>,
    mine: Vec<u32>,
}

// fn parse_cards(lines) {

// }

fn main() {
    let file_path = "input.txt";
    let contents = fs::read_to_string(file_path)
        .expect("Should have been able to read the file");

    let lines = contents.split('\n');
    let mut card_winners: Vec<i64> = Vec::new();
    let mut cards: Vec<i64> = Vec::new();

    let mut line_num = 0;
    for row in lines {
        let mut winners = HashSet::new();
        let card = row.split(":").collect::<Vec<_>>()[1]
                        .split(" | ").collect::<Vec<_>>();
        for num in card[0].split(" ") {
            if num != "" {
                winners.insert(num.to_string());
            }
        }
        let mut my_winners = 0;
        for num in card[1].split(" ") {
            if winners.contains(num) {
                my_winners += 1;
            }
        }
        card_winners.push(my_winners);
        cards.push(line_num);
        line_num += 1;
    }


    let mut res = 0;
    while cards.len() > 0 {
        let c = cards.pop().unwrap();
        res += 1;
        let winners = card_winners[c as usize];
        for next in c + 1..c + winners+1 {
            cards.push(next)
        }
    }
    
    println!("Res:\n{res}");
}