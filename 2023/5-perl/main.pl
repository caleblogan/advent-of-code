sub println {
    my $msg = shift;
    print($msg . "\n");
}

sub get_seeds {
    my $in = shift;
    my $line = <$in>;
    <$in>;
    chomp($line);
    my @nums = $line =~ /(\d+)/g;
    return @nums;
}
sub new_range {
    my $dest = shift;
    my $start = shift;
    my $range = shift;
    my %res = (
       dest => $dest,
       src_start => $start,
       src_end => $start + $range - 1
    );
    return %res;
}
sub get_map_section {
    my $in = shift;
    <$in>;
    my $line = <$in>;
    my @res;
    while ($line ne "\n" && !eof($in)) {
        chomp($line);
        my @nums = $line =~ /(\d+)/g;
        my %range = new_range @nums;
        push @res, \%range; # reference = \
        $line  = <$in>;
    }
    # println(@res[1]->{"dest"});
    return @res;
}

my $fname = $ARGV[0];
open(my $in,  "<",  $fname)  or die "Can't open input.txt: $!";
my @seeds = get_seeds $in;
my @seeds_to_soil = get_map_section $in;
my @soil_to_fert = get_map_section $in;
my @fert_to_water = get_map_section $in;
my @water_to_light = get_map_section $in;
my @light_to_temp = get_map_section $in;
my @temp_to_humid = get_map_section $in;
my @humid_to_location = get_map_section $in;

# println(@{$seeds_to_soil[0]});

# for ($i = 0; $i < @seeds_to_soil; $i++) {
#     println(join(",", @{$seeds_to_soil[$i]}));
# }



sub convert {
    my $seed = shift; # int
    my (%range) = @_; # range structure
    my $offset = $seed - $range{"src_start"};
    return $range{"dest"} + $offset;
}

sub find_dest {
    my $seed = shift; # int
    my (@map) = @_; # range structure
    my $dest = $seed;
    for (my $i = 0; $i < @map; $i++) {
        %range = %{$map[$i]};
        if ($seed >= $range{"src_start"} && $seed <= $range{"src_end"}) {
            $dest = convert $seed, %range;
        }
    }
    return $dest;
}

my $min = -1;
for (my $i = 0; $i < @seeds; $i++) {
    my $dest = find_dest $seeds[$i], @seeds_to_soil;
    $dest = find_dest $dest, @soil_to_fert;
    $dest = find_dest $dest, @fert_to_water;
    $dest = find_dest $dest, @water_to_light;
    $dest = find_dest $dest, @light_to_temp;
    $dest = find_dest $dest, @temp_to_humid;
    $dest = find_dest $dest, @humid_to_location;
    if ($min == -1 || $dest < $min) {
        $min = $dest;
    }
}
println("Mins position: ".$min);