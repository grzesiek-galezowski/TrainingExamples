using System.Collections.Generic;
using Functional.Maybe;
using Functional.Maybe.Just;
using TddXt.XNSubstitute;

namespace KataTrainReservationTddEbook;

//There are various business rules and policies around which seats may be reserved.
//For a train overall, no more than 70% of seats may be reserved in advance,
//and ideally no individual coach should have no more than 70% reserved seats either.
//However, there is another business rule that says you must put all the seats for one
//reservation in the same coach.
//This could make you and go over 70% for some coaches,
//just make sure to keep to 70% for the whole train.

// scenario 1: 70% rule for train OK, fits any 100% OK, fits any 70% OK, 70% rule for first coach OK
// coach 1: 1A(v), 1B(x), 1C(x)
// coach 2: 2A(x), 2B(x), 2C(x)
// reservation: 1 place
// outcome:
// coach 1: 1A(v), 1B(v), 1C(x)
// coach 2: 2A(x), 2B(x), 2C(x)
//IN: train id, how many seats
//{"train_id": "express_2000", "booking_reference": "75bcd15", "seats": ["1B"]}

// scenario 2: 70% rule for train OK, fits any 100% OK, fits any 70% OK, 70% rule for first coach not OK
// coach 1: 1A(v), 1B(v), 1C(v), 1D(x)
// coach 2: 2A(x), 2B(x), 2C(x)
// reservation: 1 place
// outcome:
// coach 1: 1A(v), 1B(v), 1C(v), 1D(x)
// coach 2: 2A(v), 2B(x), 2C(x)
//IN: train id, how many seats
//{"train_id": "express_2000", "booking_reference": "75bcd15", "seats": ["2A"]}


// scenario 3: 70% rule for train OK, fits any 100% OK, fits any 70% NOT OK
// coach 1: 1A(v), 1B(v), 1C(v), 1D(x)
// coach 2: 2A(x), 2B(x), 2C(x)
// reservation: 1 place
// outcome:
// coach 1: 1A(v), 1B(v), 1C(v), 1D(x)
// coach 2: 2A(v), 2B(x), 2C(x)
//IN: train id, how many seats
//{"train_id": "express_2000", "booking_reference": "75bcd15", "seats": ["2A"]}

// scenario 4: 70% rule for train OK, fits any 100% not OK
// scenario 5: 70% rule for train not OK
