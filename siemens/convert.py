
import json
import pandas as pd


new_names = {
    "Data point": "sensor",
    "Type": "type",
    "Position": "position",
    "Phenomenon": "phenomenon",
    "Unit": "unit",
    "Range": "range",
    "Remark": "remark"
}

json_sequence = ['type', 'phenomenon', 'position', 'unit', 'range', 'remark']


def main():
    """
    Convert online table to JSON
    """

    df = pd.read_csv("siemens.csv")
    df.rename(columns=new_names, inplace=True)
    duplicate = df.duplicated(subset=["sensor"])
    if any(duplicate):
        print(f"Duplicate sensors: {df[duplicate]['sensor'].to_list()}")

    df = df[~duplicate]
    df.set_index("sensor", inplace=True)
    data = df[json_sequence].to_json(orient="index")
    parsed = json.loads(data)

    # Cleanup format
    for sensor_name, sensor_data in parsed.items():
        for parameter, value in sensor_data.items():
            # Empty values
            if value is None:
                continue

            # Lower case
            if parameter in ['position', 'type', 'phenomenon']:
                sensor_data[parameter] = value.lower()

            # Cleanup range
            if parameter == 'range':
                range_values = value.split("\u2026")
                for idx, range_value in enumerate(range_values):
                    range_values[idx] = ''.join(c for c in range_value if c.isdigit())

                sensor_data[parameter] = range_values

            # Units cleanup
            if parameter == 'unit' and "\u00b0C" in value:
                sensor_data[parameter] = "C"
            if parameter == "unit" and "On | Off" in value:
                sensor_data[parameter] = "bool"

    with open("siemens.json", "w") as f:
        json.dump(parsed, f, indent=4)


if __name__ == "__main__":
    main()
